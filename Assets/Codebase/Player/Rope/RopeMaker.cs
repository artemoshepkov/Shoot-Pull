using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Player.Rope
{
    public class RopeMaker : MonoBehaviour
    {
        private int _defaultRopeLength = 3;
        
        private Rigidbody2D _rbRopeEnd;
        private HingeJoint2D _jointRopeEnd;


        [SerializeField] private List<GameObject> _middleRopeLinks = new List<GameObject>();
        [SerializeField] private int _ropeLength;
        [SerializeField] private GameObject _ropeBegin;
        [SerializeField] private GameObject _ropeEnd;
        [SerializeField] private int _maxLength = 10;
        private string _pathMiddleRopeLink;

        public int CurrentRopeLength => _middleRopeLinks.Count + 2;

        private void Awake()
        {
            _rbRopeEnd = _ropeEnd.GetComponent<Rigidbody2D>();
            _jointRopeEnd = _ropeEnd.GetComponent<HingeJoint2D>();

            _rbRopeEnd.mass = _maxLength - 1;
        }

        private void OnValidate()
        {
            if (_ropeLength < _defaultRopeLength)
                _ropeLength = _defaultRopeLength;
            
            if (_ropeLength > _maxLength)
                _ropeLength = _maxLength;
            
            if (_ropeLength > CurrentRopeLength)
                AddLinks(_ropeLength - CurrentRopeLength);
            else if (_ropeLength < CurrentRopeLength)
                RemoveLinks(CurrentRopeLength - _ropeLength);
        }

        private void AddLinks(int count)
        {
            for (int i = 0; i < count; i++) 
                AddRope();
        }

        private void AddRope()
        {
            Object prefab = Resources.Load(_pathMiddleRopeLink);
            GameObject lastLink = Instantiate(prefab, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
            var prevLastLink = _middleRopeLinks[0];
            _middleRopeLinks.Add(lastLink);
            
            var rbLastLink = lastLink.GetComponent<Rigidbody2D>();
            var joint = lastLink.GetComponent<HingeJoint2D>();

            rbLastLink.mass = prevLastLink.GetComponent<Rigidbody2D>().mass - 1f;

            joint.connectedBody = prevLastLink.GetComponent<Rigidbody2D>();
            // joint.connectedAnchor = prevLastLink.transform.position + new Vector3(0f, -0.6f, 0f);

            _ropeBegin.GetComponent<HingeJoint2D>().connectedBody = lastLink.GetComponent<Rigidbody2D>();
            _ropeBegin.GetComponent<HingeJoint2D>().connectedAnchor = lastLink.transform.position + new Vector3(0f, -0.6f, 0f);
        }

        private void RemoveLinks(int count)
        {
            for (int i = 0; i < count; i++) 
                RemoveLastLink();
        }

        private void RemoveLastLink()
        {
            
        }
    }
}