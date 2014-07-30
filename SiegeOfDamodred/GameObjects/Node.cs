using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects;

namespace GameObjects
{
    public class Node
    {
        public GameObject gameObject;
        public Node nextNode;
        public Node previousNode;

        public Node(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    public class JLinkedList
    {
        private Node mFirstNode;
        private Node mLastNode;
        private Node mPreviousNode;
        private int mCount;
        private Node mCurrent;

        public JLinkedList()
        {
            mFirstNode = null;
            mLastNode = null;
            mCount = 0;
        }

        public Node GetFirstNode()
        {
            return mFirstNode;
        }


        public int GetCount()
        {
            return mCount;
        }

        public bool IsEmpty()
        {
            return mFirstNode == null;
        }

        public Node GetCurrent()
        {
            return mCurrent;
        }

        public void NextNode()
        {

            if (GameObject.mPotentialTargetListList.GetCount() > 3)
            {

            }

            try
            {
                mPreviousNode = mCurrent;
                mCurrent = mCurrent.nextNode;
            }
            catch (Exception e)
            {

            }


        }


        public void Reset()
        {
            mCurrent = mFirstNode;
            mPreviousNode = null;
        }

        public bool AtEnd()
        {
            return (mCurrent.nextNode == null);
        }

        public GameObject FindObjectAtID(int ID)
        {
            Node currentNode = mFirstNode; // start at first node
            while (currentNode.gameObject.mObjectID != ID)
            {
                currentNode = currentNode.nextNode; // Move to next link
                if (currentNode == null)
                {
                    // Print to debug line here:
                    // This means the object we are trying to delete, was never added
                    // to the global list of in play objects.
                    return null;
                }
            }

            return currentNode.gameObject;
        }

        public void InsertFirst(GameObject gameObject)
        {
            Node newNode = new Node(gameObject);

            if (IsEmpty())
            {
                mLastNode = newNode;

            }
            else
            {
                mFirstNode.previousNode = newNode; // newNode <----- oldFirstNode
            }
            newNode.nextNode = mFirstNode; // newNode ------>oldFirstNode
            mFirstNode = newNode; // FirstNode ----> newNode
            mCount++;
        }

        public void CreateNewNode(GameObject gameObject, Node current)
        {
            Node newNode = new Node(gameObject);

            current.nextNode.previousNode = newNode;
            newNode.nextNode = current.nextNode;
            newNode.previousNode = current;
            current.nextNode = newNode;
        }

        public void InsertLast(GameObject gameObject)
        {
            Node newNode = new Node(gameObject);

            if (IsEmpty())
            {
                mFirstNode = newNode; // FirstNode == new Node

            }
            else
            {
                mLastNode.nextNode = newNode; // oldLastNode ---> newNode
                newNode.previousNode = mLastNode; // previousNode --->  LastNode
            }
            mLastNode = newNode; // NewLink <---last
            mCount++;
        }

        public GameObject DeleteFirst()
        {
            GameObject tempGameObject = mFirstNode.gameObject;

            if (mFirstNode.nextNode == null)
            {
                mLastNode = null;

            }

            mFirstNode = mFirstNode.nextNode;
            mCount--;
            return tempGameObject;
        }


        public Node DeleteAt(int ID)
        {
            Node currentNode = mFirstNode; // start at first node
            while (currentNode.gameObject.mObjectID != ID)
            {
                currentNode = currentNode.nextNode; // Move to next link
                if (currentNode == null)
                {
                    // Print to debug line here:
                    // This means the object we are trying to delete, was never added
                    // to the global list of in play objects.
                    return null;
                }
            }

            //Console.WriteLine(currentNode.gameObject.Sprite.AssetName);
            currentNode.gameObject = null;
            //Console.WriteLine(currentNode.gameObject);
            if (currentNode == mFirstNode) //Found it on first try
            {
                mFirstNode = currentNode.nextNode;
            }
            else
            {
                currentNode.previousNode.nextNode = currentNode.nextNode;
            }

            if (currentNode == mLastNode) // Found it on last node
            {
                mLastNode = currentNode.previousNode;
            }
            else
            {
                currentNode.nextNode.previousNode = currentNode.previousNode;
            }


            mCount--;

            return currentNode;
        }
    }




    //TODO: Create a display mButtonAction for debugging using DebugLib



    public class JTargetQueue
    {

        public JLinkedList mTargetList;
        private Node mCurrent;
        private int mCount;
        private Node mFirstNode;

        public JTargetQueue()
        {
            mTargetList = new JLinkedList();
            mCount = 0;
            mCurrent = null;
        }




        public void DisplayList()
        {
            Node current = mTargetList.GetFirstNode();

            while (current != null)
            {

                current = current.nextNode;
            }

        }

        public GameObject FindObjectAtID(int ID)
        {
            Node currentNode = mTargetList.GetFirstNode(); // start at first node
            while (currentNode.gameObject.mObjectID != ID)
            {
                currentNode = currentNode.nextNode; // Move to next link
                if (currentNode == null)
                {
                    // Print to debug line here:
                    // This means the object we are trying to delete, was never added
                    // to the global list of in play objects.
                    return null;
                }
            }

            return currentNode.gameObject;
        }


        public bool IsEmpty()
        {
            return mTargetList.IsEmpty();
        }

        
        public void InsertTarget(GameObject target) 
        {

            mTargetList.InsertLast(target);
        }

        public GameObject PeekFirst()
        {
            if (mTargetList.GetFirstNode() != null)
            {
                return mTargetList.GetFirstNode().gameObject;
            }
            return null;
        }

        public GameObject PopTarget()
        {
            return mTargetList.DeleteFirst();
        }

    }

}
