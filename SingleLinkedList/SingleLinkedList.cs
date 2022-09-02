using System.Diagnostics;

namespace SingleLinkedList
{
    [DebuggerDisplay("Count={Count}")]
    public class SingleLinkedList<T>
    {
        // Fields
        private int m_count;
        private SingleLinkedListNode<T> m_head;
        private SingleLinkedListNode<T> m_tail;
        private int m_updateCode;

        // Constructors

        /// <summary>
        /// Initializes a new instance of the SingleLinkedList(T) class that is empty.
        /// </summary>
        public SingleLinkedList()
        {

        }

        /// <summary>
        /// Initializes a new instance of the SingleLinkedList(T) class that contains the items in the array.
        /// </summary>
        /// <param name="items">Adds the items to the end of the SingleLinkedList(T).</param>
        public SingleLinkedList(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                AddToEnd(item);
            }
        }

        // Methods

        /// <summary>
        /// Adds the specified value to the SingleLinkedList(T) after the specified node.
        /// </summary>
        /// <param name="node">The node to add the value after.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The newly created node that holds the value.</returns>
        public SingleLinkedListNode<T> AddAfter(SingleLinkedListNode<T> node, T value)
        {
            SingleLinkedListNode<T> newNode = new SingleLinkedListNode<T>(this, value);

            AddAfter(node, newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified newNode to the SingleLinkedList(T) after the specified node.
        /// </summary>
        /// <param name="node">The node to add the newNode after.</param>
        /// <param name="newNode">The node to add.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddAfter(SingleLinkedListNode<T> node, SingleLinkedListNode<T> newNode)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (newNode == null)
            {
                throw new ArgumentNullException("newNode");
            }

            if (node.Owner != this)
            {
                throw new InvalidOperationException("node is not owned by this list");
            }

            if (newNode.Owner != this)
            {
                throw new InvalidOperationException("newNode is not owned by this list");
            }

            // The newly added node becomes the tail if you are adding after the tail
            if (m_tail == node)
            {
                m_tail = newNode;
            }

            newNode.Next = node.Next;
            node.Next = newNode;

            ++m_count;
            ++m_updateCode;
        }

        /// <summary>
        /// Adds the specified value to the SingleLinkedList(T) before the specified node.
        /// </summary>
        /// <param name="node">The node to add the value before.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The newly created node that holds the value.</returns>
        public SingleLinkedListNode<T> AddBefore(SingleLinkedListNode<T> node, T value)
        {
            SingleLinkedListNode<T> newNode = new SingleLinkedListNode<T>(this, value);

            AddBefore(node, newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified newNode to the SingleLinkedList(T) before the specified node.
        /// </summary>
        /// <param name="node">The node to add the value before.</param>
        /// <param name="newNode">The value to add.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddBefore(SingleLinkedListNode<T> node, SingleLinkedListNode<T> newNode)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (newNode == null)
            {
                throw new ArgumentNullException("newNode");
            }

            if (node.Owner != this)
            {
                throw new InvalidOperationException("node is not owned by this list");
            }

            if (newNode.Owner != this)
            {
                throw new InvalidOperationException("newNode is not owned by this list");
            }

            if (m_head == node)
            {
                newNode.Next = m_head;
                m_head = newNode;
            }
            else
            {
                SingleLinkedListNode<T> beforeNode = m_head;

                while (beforeNode != null && beforeNode.Next != node)
                {
                    beforeNode = beforeNode.Next;
                }

                if (beforeNode == null)
                {
                    throw new InvalidOperationException("something went wrong");
                }

                newNode.Next = node;
                beforeNode.Next = newNode;
            }
        }

        /// <summary>
        /// Adds the value to the beginning of the SingleLinkedList(T).
        /// </summary>
        /// <param name="value">The value to add to the beginning of the SingleLinkedList(T).</param>
        /// <returns>The newly created node that is holding the value.</returns>
        public SingleLinkedListNode<T> AddToBeginning(T value)
        {
            SingleLinkedListNode<T> newNode = new SingleLinkedListNode<T>(this, value);

            if (IsEmpty)
            {
                m_head = newNode;
                m_tail = newNode;
            }
            else
            {
                newNode.Next = m_head;
                m_head = newNode;
            }

            ++m_count;
            ++m_updateCode;

            return newNode;
        }

        /// <summary>
        /// Adds the value to the end of the SingleLinkedList(T).
        /// </summary>
        /// <param name="value">The value to add to the end of the SingleLinkedList(T).</param>
        /// <returns>The newly created node that is holding the value.</returns>
        public SingleLinkedListNode<T> AddToEnd(T value)
        {
            SingleLinkedListNode<T> newNode = new SingleLinkedListNode<T>(this, value);

            if (IsEmpty)
            {
                m_head = newNode;
                m_tail = newNode;
            }
            else
            {
                m_tail.Next = newNode;
                m_tail = newNode;
            }

            ++m_count;
            ++m_updateCode;

            return newNode;
        }

        /// <summary>
        /// Removes all items from the SingleLinkedList(T).
        /// </summary>
        public void Clear()
        {
            SingleLinkedListNode<T> tmp;

            // Clean up the items in the list
            for (SingleLinkedListNode<T> node = m_head; node != null;)
            {
                tmp = node.Next;

                // Change the count and head pointer in case we throw an exception. 
                // This way the node is removed before we clear the data
                m_head = tmp;
                --m_count;

                // Erase the contents of the node
                node.Next = null;
                node.Owner = null;

                // Move to the next node
                node = tmp;
            }

            if (m_count <= 0)
            {
                m_head = null;
                m_tail = null;
            }

            ++m_updateCode;
        }

        /// <summary>
        /// Checks if the specified data is present in the SingleLinkedList(T).
        /// </summary>
        /// <param name="data">the data to look for.</param>
        /// <returns>True if the data is found, false otherwise.</returns>
        public bool Contains(T data)
        {
            return Find(data) != null;
        }

        /// <summary>
        /// Locates the first node that contains the specified data.
        /// </summary>
        /// <param name="data">The data to find.</param>
        /// <returns>The node that contains the specified data, null otherwise.</returns>
        public SingleLinkedListNode<T> Find(T data)
        {
            if (IsEmpty)
            {
                return null;
            }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            // Traverse the list from the Head to Tail.
            for (SingleLinkedListNode<T> curr = Head; curr != null; curr = curr.Next)
            {
                // Return the node we are currently on if it contains the data we are looking for.
                if (comparer.Equals(curr.Data, data))
                {
                    return curr;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the first occurence of the specified items from the SingleLinkedList(T).
        /// </summary>
        /// <param name="item">The item to remove from the SingleLinkedList(T).</param>
        /// <returns>True if an item was removed, from otherwise.</returns>
        public bool Remove(T item)
        {
            return Remove(item, false);
        }

        /// <summary>
        /// Removes the specified node from the SingleLinkedList(T).
        /// </summary>
        /// <param name="node">The node to remove from the SingleLinkedList(T).</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void Remove(SingleLinkedListNode<T> node)
        {
            if (IsEmpty)
            {
                return;
            }

            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.Owner != this)
            {
                throw new InvalidOperationException("The node doesn't belong to this list.");
            }

            SingleLinkedListNode<T> prev = null;
            SingleLinkedListNode<T> curr = Head;

            // Find the node located before the specified node by traversing the list.
            while (curr != null && curr != node)
            {
                prev = curr;
                curr = curr.Next;
            }

            // The node has been found if the current node equals the node we are looking for
            if (curr == node)
            {
                // Assign the head to the next node if the specified node is the head
                if (m_head == node)
                {
                    m_head = node.Next;
                }

                // Assign the tail to the previous node if the specified node is the tail
                if (m_tail == node)
                {
                    m_tail = prev;
                }

                // Set the previous node next reference to the removed node's next reference.
                if (prev != null)
                {
                    prev.Next = curr.Next;
                }

                // Null out the removed node's next pointer to be safe.
                node.Next = null;
                node.Owner = null;

                --m_count;
                ++m_updateCode;
            }
        }

        /// <summary>
        /// Removes the first or all occurences of the specified item from the SingleLinkedList(T).
        /// </summary>
        /// <param name="item">The item to remove from the SingleLinkedList(T).</param>
        /// <param name="allOccurences">True if all nodes should be removed that contain the specified item, False otherwise.</param>
        /// <returns>True if an item was removed, False otherwise.</returns>
        public bool Remove(T item, bool allOccurences)
        {
            if (IsEmpty)
            {
                return false;
            }

            SingleLinkedListNode<T> prev = null;
            SingleLinkedListNode<T> curr = Head;
            bool removed = false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            // Start traversing the list at the head
            while (curr != null)
            {
                // Check to see if the current node contains the data we are trying to delete
                if (!comparer.Equals(curr.Data, item))
                {
                    // Assign the current node to the previous node and the previous node to the current node
                    prev = curr;
                    curr = curr.Next;
                    continue;
                }

                // Create a pointer to the next node in the previous node
                if (prev != null)
                {
                    prev.Next = curr.Next;
                }

                if (curr == Head)
                {
                    // If the current node is the head we will have to assign the next node as the head
                    Head = curr.Next;
                }

                if (curr == Tail)
                {
                    // If the current node is the tail we will have to assign the previous node as the tail
                    Tail = prev;
                }

                // Save the pointer for clean up later
                SingleLinkedListNode<T> tmp = curr;

                // Advance the current to the next node
                curr = curr.Next;

                // Since the node will no longer be used clean up the pointers in it
                tmp.Next = null;
                tmp.Owner = null;

                // Decrement the counter since we have removed a node
                --m_count;

                removed = true;

                if (!allOccurences)
                {
                    break;
                }
            }

            if (removed)
            {
                ++m_updateCode;
            }

            return removed;
        }

        /// <summary>
        /// Copies the elements of the SingleLinkedList<T> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the SingleLinkedList<T>.</returns>
        public T[] ToArray()
        {
            T[] retVal = new T[m_count];

            int index = 0;
            for (SingleLinkedListNode<T> i = Head; i != null; i = i.Next)
            {
                retVal[index] = i.Data;
                ++index;
            }

            return retVal;
        }

        /// <summary>
        /// Gets the number of nodes actually contained in the SingleLinkedList(T).
        /// </summary>
        public int Count
        {
            get { return m_count; }
        }

        /// <summary>
        /// Gets the head node in the SingleLinkedList(T).
        /// </summary>
        public SingleLinkedListNode<T> Head
        {
            get { return m_head; }
            private set { m_head = value; }
        }

        /// <summary>
        /// States if the SingleLinkedList(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return m_count <= 0; }
        }

        /// <summary>
        /// Gets the tail node in the SingleLinkedList(T).
        /// </summary>
        public SingleLinkedListNode<T> Tail
        {
            get { return m_tail; }
            private set { m_tail = value; }
        }
    }
}
