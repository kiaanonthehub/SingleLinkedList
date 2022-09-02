using System.Diagnostics;

namespace SingleLinkedList
{
    /// <summary>
    /// Represents a node in a SingleLinkedList(T).
    /// </summary>
    /// <typeparam name="T">Specifies the type of data in the node.</typeparam>
    [DebuggerDisplay("Data={Data}")]
    public class SingleLinkedListNode<T>
    {
        SingleLinkedList<T> m_owner;
        SingleLinkedListNode<T> m_next;
        T m_data;

        /// <summary>
        /// Initializes a new instance of the SingleLinkedList(T) class with the specified data.
        /// </summary>
        /// <param name="data">The data that this node will contain.</param>
        public SingleLinkedListNode(T data)
        {
            m_data = data;
        }

        /// <summary>
        /// Initializes a new instance of the SingleLinkedList(T) class with the specified data and owner.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="data">The data that this node will contain.</param>
        internal SingleLinkedListNode(SingleLinkedList<T> owner, T data)
        {
            m_data = data;
            m_owner = owner;
        }

        /// <summary>
        /// Returns the next node.
        /// </summary>
        public SingleLinkedListNode<T> Next
        {
            get { return m_next; }
            internal set { m_next = value; }
        }

        /// <summary>
        /// Gets or sets the owner of the node
        /// </summary>
        internal SingleLinkedList<T> Owner
        {
            get { return m_owner; }
            set { m_owner = value; }
        }

        /// <summary>
        /// Gets the data contained in the node
        /// </summary>
        public T Data
        {
            get { return m_data; }
            internal set { m_data = value; }
        }
    }
}
