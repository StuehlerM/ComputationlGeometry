using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    public class SweepLine : ICollection<Vector2>, IList<Vector2>
    {
        #region Segment
        public class Segment : ICollection<Vector2>, IList<Vector2>
        {
            public Segment(Vector2 value, SweepLine sweepLine)
            {
                this.Value = value;
                this.Level = 1;
                this.Count = 1;
                this.SweepLine = sweepLine;
            }

            public SweepLine SweepLine { get; private set; }
            public Vector2 Value { get; private set; }
            public Segment Parent { get; private set; }
            public Segment LeftHand { get; private set; }
            public Segment RightHand { get; private set; }
            int Level { get; set; }
            public int Count { get; private set; }

            public void Add(Vector2 item)
            {
                var compare = item.CompareTo(this.Value, this.SweepLine.xPosition);
                if (compare < 0)
                    if (this.LeftHand == null)
                        ((this.LeftHand = new SweepLine.Segment(item, this.SweepLine)).Parent = this).Reconstruct(true);
                    else this.LeftHand.Add(item);
                else
                    if (this.RightHand == null)
                    ((this.RightHand = new SweepLine.Segment(item, this.SweepLine)).Parent = this).Reconstruct(true);
                else this.RightHand.Add(item);
            }

            public void Clear()
            {
                if (this.LeftHand != null) this.LeftHand.Clear();
                if (this.RightHand != null) this.RightHand.Clear();
                this.LeftHand = this.RightHand = null;
            }

            public bool Contains(Vector2 item)
            {
                var compare = item.CompareTo(this.Value, this.SweepLine.xPosition);
                if (compare < 0)
                    return this.LeftHand == null ? false : this.LeftHand.Contains(item);
                else if (compare == 0)
                    return true;
                else
                    return this.RightHand == null ? false : this.RightHand.Contains(item);
            }

            public void CopyTo(Vector2[] array, int arrayIndex)
            {
                if (this.LeftHand != null)
                {
                    this.LeftHand.CopyTo(array, arrayIndex);
                    arrayIndex += this.LeftHand.Count;
                }
                array[arrayIndex++] = this.Value;
                if (this.RightHand != null)
                    this.RightHand.CopyTo(array, arrayIndex);
            }

            public bool IsReadOnly { get { return false; } }

            public bool Remove(Vector2 item)
            {
                var compare = item.CompareTo(this.Value, this.SweepLine.xPosition);
                if (compare == 0)
                {
                    if (this.LeftHand == null && this.RightHand == null)
                        if (this.Parent != null)
                        {
                            if (this.Parent.LeftHand == this) this.Parent.LeftHand = null;
                            else this.Parent.RightHand = null;
                            this.Parent.Reconstruct(true);
                        }
                        else this.SweepLine.RootNode = null;
                    else if (this.LeftHand == null || this.RightHand == null)
                    {
                        var child = this.LeftHand == null ? this.RightHand : this.LeftHand;
                        if (this.Parent != null)
                        {
                            if (this.Parent.LeftHand == this) this.Parent.LeftHand = child;
                            else this.Parent.RightHand = child;
                            (child.Parent = this.Parent).Reconstruct(true);
                        }
                        else (this.SweepLine.RootNode = child).Parent = null;
                    }
                    else
                    {
                        var replace = this.LeftHand;
                        while (replace.RightHand != null) replace = replace.RightHand;
                        var temp = this.Value;
                        this.Value = replace.Value;
                        replace.Value = temp;
                        return replace.Remove(replace.Value);
                    }
                    this.Parent = this.LeftHand = this.RightHand = null;
                    return true;
                }
                else if (compare < 0)
                    return this.LeftHand == null ? false : this.LeftHand.Remove(item);
                else
                    return this.RightHand == null ? false : this.RightHand.Remove(item);
            }

            public IEnumerator<Vector2> GetEnumerator()
            {
                if (this.LeftHand != null)
                    foreach (var item in this.LeftHand)
                        yield return item;
                yield return this.Value;
                if (this.RightHand != null)
                    foreach (var item in this.RightHand)
                        yield return item;
            }

            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

            void Reconstruct(bool recursive)
            {
                this.Count = 1;

                int leftLevel = 0, rightLevel = 0;
                if (this.LeftHand != null)
                {
                    leftLevel = this.LeftHand.Level;
                    this.Count += this.LeftHand.Count;
                }
                if (this.RightHand != null)
                {
                    rightLevel = this.RightHand.Level;
                    this.Count += this.RightHand.Count;
                }

                if (leftLevel - rightLevel > 1)
                {
                    var leftLeft = this.LeftHand.LeftHand == null ? 0 : this.LeftHand.LeftHand.Level;
                    var leftRight = this.LeftHand.RightHand == null ? 0 : this.LeftHand.RightHand.Level;
                    if (leftLeft >= leftRight)
                    {
                        this.LeftHand.Elevate();
                        this.Reconstruct(true);
                    }
                    else
                    {
                        var pivot = this.LeftHand.RightHand;
                        pivot.Elevate(); pivot.Elevate();
                        pivot.LeftHand.Reconstruct(false);
                        pivot.RightHand.Reconstruct(true);
                    }
                }
                else if (rightLevel - leftLevel > 1)
                {
                    var rightRight = this.RightHand.RightHand == null ? 0 : this.RightHand.RightHand.Level;
                    var rightLeft = this.RightHand.LeftHand == null ? 0 : this.RightHand.LeftHand.Level;
                    if (rightRight >= rightLeft)
                    {
                        this.RightHand.Elevate();
                        this.Reconstruct(true);
                    }
                    else
                    {
                        var pivot = this.RightHand.LeftHand;
                        pivot.Elevate(); pivot.Elevate();
                        pivot.LeftHand.Reconstruct(false);
                        pivot.RightHand.Reconstruct(true);
                    }
                }
                else
                {
                    this.Level = Math.Max(leftLevel, rightLevel) + 1;
                    if (this.Parent != null && recursive)
                        this.Parent.Reconstruct(true);
                }
            }

            void Elevate()
            {
                var root = this.Parent;
                var parent = root.Parent;
                if ((this.Parent = parent) == null) this.SweepLine.RootNode = this;
                else
                {
                    if (parent.LeftHand == root) parent.LeftHand = this;
                    else parent.RightHand = this;
                }

                if (root.LeftHand == this)
                {
                    root.LeftHand = this.RightHand;
                    if (this.RightHand != null) this.RightHand.Parent = root;
                    this.RightHand = root;
                    root.Parent = this;
                }
                else
                {
                    root.RightHand = this.LeftHand;
                    if (this.LeftHand != null) this.LeftHand.Parent = root;
                    this.LeftHand = root;
                    root.Parent = this;
                }
            }

            public int IndexOf(Vector2 item)
            {
                var compare = item.CompareTo(this.Value, this.SweepLine.xPosition);
                if (compare == 0)
                    if (this.LeftHand == null) return 0;
                    else
                    {
                        var temp = this.LeftHand.IndexOf(item);
                        return temp == -1 ? this.LeftHand.Count : temp;
                    }
                else if (compare < 0)
                    if (this.LeftHand == null) return -1;
                    else return this.LeftHand.IndexOf(item);
                else
                    if (this.RightHand == null) return -1;
                else return this.RightHand.IndexOf(item);
            }

            public void Insert(int index, Vector2 item) { throw new InvalidOperationException(); }

            public void RemoveAt(int index)
            {
                if (this.LeftHand != null)
                    if (index < this.LeftHand.Count)
                    {
                        this.LeftHand.RemoveAt(index);
                        return;
                    }
                    else index -= this.LeftHand.Count;
                if (index-- == 0)
                {
                    this.Remove(this.Value);
                    return;
                }
                if (this.RightHand != null)
                    if (index < this.RightHand.Count)
                    {
                        this.RightHand.RemoveAt(index);
                        return;
                    }
                throw new ArgumentOutOfRangeException("index");
            }

            public Vector2 this[int index] {
                get {
                    if (this.LeftHand != null)
                        if (index < this.LeftHand.Count) return this.LeftHand[index];
                        else index -= this.LeftHand.Count;
                    if (index-- == 0) return this.Value;
                    if (this.RightHand != null)
                        if (index < this.RightHand.Count) return this.RightHand[index];
                    throw new ArgumentOutOfRangeException("index");
                }
                set { throw new InvalidOperationException(); }
            }

            public Segment getSegment(Vector2 item)
            {
                var compare = item.CompareTo(this.Value, this.SweepLine.xPosition);
                if (compare < 0)
                    return this.LeftHand == null ? null : this.LeftHand.getSegment(item);
                else if (compare == 0)
                    return this;
                else
                    return this.RightHand == null ? null : this.RightHand.getSegment(item);
            }

            public void Switch(Segment other)
            {
                Vector2 temp = this.Value;
                this.Value = other.Value;
                other.Value = temp;
            }
        }
        #endregion

        public Segment RootNode { get; private set; }
        public double xPosition { get; set; }

        public void Add(Vector2 item)
        {
            if (this.RootNode == null) this.RootNode = new SweepLine.Segment(item, this);
            else this.RootNode.Add(item);
        }

        public void Clear()
        {
            if (this.RootNode == null) return;
            this.RootNode.Clear();
            this.RootNode = null;
        }

        public bool Contains(Vector2 item) { return this.RootNode == null ? false : this.RootNode.Contains(item); }

        public void CopyTo(Vector2[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex");
            if ((array.Length <= arrayIndex) || (this.RootNode != null && array.Length < arrayIndex + this.RootNode.Count))
                throw new ArgumentException();

            if (this.RootNode != null)
                this.RootNode.CopyTo(array, arrayIndex);
        }

        public int Count { get { return this.RootNode.Count; } }

        public bool IsReadOnly { get { return false; } }

        public bool Remove(Vector2 item) { return this.RootNode == null ? false : this.RootNode.Remove(item); }

        public IEnumerator<Vector2> GetEnumerator()
        {
            if (this.RootNode != null)
                foreach (var item in this.RootNode)
                    yield return item;
            else
                yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public int IndexOf(Vector2 item) { return this.RootNode != null ? this.RootNode.IndexOf(item) : -1; }

        public void Insert(int index, Vector2 item) { throw new InvalidOperationException(); }

        public void RemoveAt(int index) { if (this.RootNode != null) this.RootNode.RemoveAt(index); }

        public Vector2 this[int index] {
            get {
                if (this.RootNode != null) return this.RootNode[index];
                else throw new ArgumentOutOfRangeException("index");
            }
            set { throw new InvalidOperationException(); }
        }

        Segment getSegment(Vector2 item)
        {
            return this.RootNode == null ? null : this.RootNode.getSegment(item);
        }

        public Vector2 Above(Vector2 item)
        {
            Segment seg = getSegment(item);

            if (seg.RightHand != null)
                return findMinimum(seg.RightHand).Value;

            Segment y = seg.Parent;
            Segment x = seg;
            while (y != null && x == y.RightHand)
            {
                x = y;
                y = y.Parent;
            }
            // Intuition: as we traverse left up the tree we traverse smaller values
            // The first node on the right is the next larger number
            if(y == null)
            {
                return null;

            }
            return y.Value;
        }

        public Vector2 Below(Vector2 item)
        {
            Segment seg = getSegment(item);

            if (seg.LeftHand != null)
                return findMaximum(seg.LeftHand).Value;

            Segment parent = seg.Parent;

            Segment y = parent;
            Segment x = seg;
            while (y != null && x == y.LeftHand)
            {
                x = y;
                y = y.Parent;
            }

            if (y == null)
            {
                return null;

            }
            return y.Value; ;
        }

        Segment findMinimum(Segment seg)
        {
            Segment current = seg;

            /* loop down to find the leftmost leaf */
            while (current.LeftHand != null)
            {
                current = current.LeftHand;
            }
            return current;
        }

        Segment findMaximum(Segment seg)
        {
            Segment current = seg;

            /* loop down to find the leftmost leaf */
            while (current.RightHand != null)
            {
                current = current.RightHand;
            }
            return current;
        }

        public void Switch(Vector2 item1, Vector2 item2)
        {
            Segment seg1 = getSegment(item1);
            Segment seg2 = getSegment(item2);

            if(seg1 != null && seg2 != null)
            {
                seg1.Switch(seg2);
            }

        }
    }
}
