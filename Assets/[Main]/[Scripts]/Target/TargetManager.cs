using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class TargetManager : MonoBehaviour
    {
        [SerializeField] private TargetHandler targetToSpawn;
        [SerializeField] private Transform targetsParent;
        [SerializeField] private float timeToSpawn;
        [SerializeField] private FloatEntityData speed;
        [SerializeField] private Vector3EntityData direction;
        [SerializeField] private ColorEntityData rightColor;
        [SerializeField] private ColorEntityData leftColor;
        [SerializeField] private StringEntityData rightOrientation;
        [SerializeField] private StringEntityData leftOrientation;
        [SerializeField] private int maxConsecutiveSpawn;
        [SerializeField] private float minSpawnTime;
        [SerializeField] private Transform[] rightArea;
        [SerializeField] private Transform[] leftArea;

        private float timeLeft;
        private bool right = true;
        private float lastSpawnTime;
        private int currentConsecutiveSpawn = 0;
        private PoolManager<TargetHandler> targetPool;

        public void SpawnRight()
        {
            SpawnRandom(rightArea, rightColor.Value, rightOrientation);
        }

        public void SpawnLeft()
        {
            SpawnRandom(leftArea, leftColor.Value, leftOrientation);
        }

        public void SpawnRandom()
        {
            if (currentConsecutiveSpawn >= maxConsecutiveSpawn)
            {
                if (Time.time - lastSpawnTime < minSpawnTime)
                {
                    return;
                }
                currentConsecutiveSpawn = 0;
            }

            lastSpawnTime = Time.time;
            currentConsecutiveSpawn++;
            if (right) SpawnRight();
            else SpawnLeft();
            right = !right;
        }

        public void SpawnRandom(Transform[] area, Color color, StringEntityData orientation)
        {
            if (targetPool == null)
            {
                targetPool = new PoolManager<TargetHandler>(targetToSpawn, targetsParent);
            }
            var instance = targetPool.GetInstance();

            instance.transform.position = area[Random.Range(0, area.Length)].position;

            instance.OnDie.AddListener(CollectTarget);
            instance.StartMoving(direction.Value, speed.Value, color, orientation);
        }

        public void CollectTarget(SpawnableHandler target)
        {
            target.OnDie?.RemoveListener(CollectTarget);
            targetPool.Destroy((TargetHandler)target);
        }
    }
}
