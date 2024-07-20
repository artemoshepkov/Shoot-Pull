using UnityEngine;

namespace Codebase.Player.Rope
{
    public class Link : MonoBehaviour
    {
        [SerializeField] private Vector3 _pointToConnect;
        
        public Rigidbody2D Rb;
        public HingeJoint2D HingeJoint2D;

        public void ConnectToLink(Link link)
        {
            HingeJoint2D.connectedBody = link.Rb;            
            HingeJoint2D.connectedAnchor = link._pointToConnect;
        }
    }
}