using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Player.Rope
{
    public class RopeContainer : MonoBehaviour
    {
        private string _pathHeadLink = "Prefabs/HeadLink";
        private string _pathMiddleLink = "Prefabs/MiddleLink";
        private string _pathTailRopeLink = "Prefabs/TailLink";
        private int _defaultLength = 2;

        private LinkedList<Link> _links = new LinkedList<Link>();

        [SerializeField] private int _maxLength;
        [SerializeField] private Transform _transform;

        public float MinHeight => 1f;
        public float MaxWeight => _maxLength;

        private void Awake()
        {
            CreateRope();
            for (int i = 0; i < 2; i++)
                AddLink();
            
            RemoveLink();
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(new Vector2(0f, 0f), new Vector2(100f, 50f)), "Add link"))
                AddLink();
            
            if (GUI.Button(new Rect(new Vector2(0f, 75f), new Vector2(100f, 50f)), "Remove link"))
                RemoveLink();
        }

        private void CreateRope()
        {
            var head = AssetProvider.Instantiate(_pathHeadLink, _transform.position, Quaternion.identity,
                this.transform).GetComponent<Link>();
            _links.AddFirst(head);
            head.Rb.mass = MinHeight;
            
            var tail = AssetProvider.Instantiate(_pathTailRopeLink, _transform.position, Quaternion.identity,
                this.transform).GetComponent<Link>();
            _links.AddLast(tail);
            tail.Rb.mass = MaxWeight;
            
            head.ConnectToLink(tail);
        }

        private void AddLink()
        {
            if (_links.Count >= _maxLength)
                return;
            
            var newLink = AssetProvider.Instantiate(_pathMiddleLink, _transform.position, Quaternion.identity,
                this.transform).GetComponent<Link>();
            newLink.Rb.mass = MaxWeight - _links.Count;
            
            var prevLink = _links.First.Next.Value;

            _links.AddAfter(_links.First, newLink);
            
            _links.First.Value.ConnectToLink(newLink);
            newLink.ConnectToLink(prevLink);
        }
        
        private void RemoveLink()
        {
            if (_links.Count <= _defaultLength)
                return;
            
            var linkToRemove = _links.First.Next;
            var nextLink = linkToRemove.Next;
            
            _links.Remove(linkToRemove);
            Destroy(linkToRemove.Value.gameObject);
            
            _links.First.Value.ConnectToLink(nextLink.Value);
        }
    }
}