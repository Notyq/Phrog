using Pathfinding;
using UnityEngine;

namespace Enemies
{
    public class EnemyPathfinding : MonoBehaviour
    {
        public AIPath aiPath;

        // Update is called once per frame
        void Update()
        {
            if (aiPath.desiredVelocity.x >= .01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
