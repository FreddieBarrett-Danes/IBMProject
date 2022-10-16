using UnityEngine;

namespace BT
{
    public abstract class BT_Tree : MonoBehaviour
    {

        private BT_Node root;

        protected void Start()
        {
            root = SetupTree();
        }

        private void Update()
        {
            root?.Evaluate();
        }

        protected abstract BT_Node SetupTree();
    }
}
