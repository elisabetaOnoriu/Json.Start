﻿namespace LinkedList
{
    public class Node<T>
    {
        CircularDoublyLinkedList<T> list = null;
        Node<T> next;
        Node<T> previous;
        T data;

        public Node(T data)
        {
            this.data = data;
        }

        public CircularDoublyLinkedList<T> List { get => list; }

        internal CircularDoublyLinkedList<T> SetList { set => list = value; }

        public Node<T> Next { get => next; }

        internal Node<T> SetNext { set => next = value; }

        public T Data { get => data; }

        public Node<T> Previous { get => previous; }

        internal Node<T> SetPrevious { set => previous = value; }
    }
}
