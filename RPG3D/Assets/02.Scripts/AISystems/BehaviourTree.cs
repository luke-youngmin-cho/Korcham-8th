using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public class BehaviourTree : MonoBehaviour
    {
        public Blackboard blackboard;
        public Stack<Node> stack;
        public Root root;
        public bool isBusy;

        private void Update()
        {
            if (isBusy)
                return;

            isBusy = true;
            StartCoroutine(C_Tick());
        }

        IEnumerator C_Tick()
        {
            stack.Push(root);

            while (stack.Count > 0)
            {
                Node current = stack.Pop();
                Result result = current.Invoke();

                if (result == Result.Running)
                {
                    stack.Push(current);
                    yield return null;
                }
            }

            isBusy = false;
        }


        #region Builder
        private Node _current;
        private Stack<Composite> _compositeBuiltStack;

        public BehaviourTree StartBuild()
        {
            blackboard = new Blackboard(this);
            stack = new Stack<Node>();
            _compositeBuiltStack = new Stack<Composite>();
            _current = root = new Root(this);
            return this;
        }

        public BehaviourTree Sequence()
        {
            Composite node = new Sequence(this);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree Selector()
        {
            Composite node = new Selector(this);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree Parallel(Parallel.Policy successPolicy)
        {
            Composite node = new Parallel(this, successPolicy);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree Decorator(Func<bool> condition)
        {
            Node node = new Decorator(this, condition);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree Execution(Func<Result> execute)
        {
            Node node = new Execution(this, execute);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree Seek(float radius, float height, float angle, LayerMask targetMask, float maxDistance)
        {
            Node node = new Seek(this, radius, height, angle, targetMask, maxDistance);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree ExitCurrentComposite()
        {
            if (_compositeBuiltStack.Count > 0)
            {
                _compositeBuiltStack.Pop();
                _current = _compositeBuiltStack.Count > 0 ? _compositeBuiltStack.Peek() : null;
            }
            else
            {
                throw new Exception($"[Tree] : Composite stack is empty.");
            }

            return this;
        }

        private void Attach(Node parent, Node child)
        {
            // parent 에 child 붙이기
            if (parent is IParentOfChild)
            {
                ((IParentOfChild)parent).child = child;
            }
            else if (parent is IParentOfChildren)
            {
                ((IParentOfChildren)parent).children.Add(child);
            }
            else
            {
                throw new System.Exception($"[Tree] : Can't attach child to {parent.GetType()}.");
            }

            // current 갱신 (그다음 자식을 이어서 붙일 위치 갱신)
            if (child is IParentOfChild)
            {
                _current = child;
            }
            else if (child is IParentOfChildren)
            {
                _current = child;
                _compositeBuiltStack.Push((Composite)child);
            }
            else
            {
                // 자식이 없는 노드는 생성후 가장 최근에 생성한 컴포지트로 돌아가서 자식을 이어서 추가할 수 있도록 한다.
                _current = _compositeBuiltStack.Count > 0 ? _compositeBuiltStack.Peek() : null;
            }
        }

        #endregion
    }
}