using System.Collections.Generic;
using UnityEngine;

public class ObstacleTransparency : MonoBehaviour
{
    public enum Mode { Opacity, Scale }

    [Header("Targets")]
    public Transform player;
    public List<string> targetTags = new List<string> {};

    [Header("Obstacle Behavior")]
    public Mode mode = Mode.Scale;
    [Range(0f, 1f)] public float opacity = 0.3f;
    [Range(0.01f, 1f)] public float targetScale = 0.1f;
    public float fadeSpeed = 5f;

    private class ObstacleData
    {
        public Vector3 origPos, origScale, origColSize, origColCenter;
        public float origBottomY;
        public float progress; // 0 = normal, 1 = shrink/transparent
        public bool isBlocking;
    }

    private Dictionary<Transform, ObstacleData> tracked = new Dictionary<Transform, ObstacleData>();

    private void Update()
    {
        foreach (var data in tracked.Values)
            data.isBlocking = false;

        HashSet<Transform> targets = new HashSet<Transform>();
        if (player != null) targets.Add(player);
        foreach (string tag in targetTags)
            if (!string.IsNullOrEmpty(tag))
                foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                    targets.Add(go.transform);

        foreach (Transform target in targets)
        {
            Vector3 dir = target.position - transform.position;
            foreach (RaycastHit hit in Physics.RaycastAll(transform.position, dir, dir.magnitude))
            {
                Transform obs = hit.transform;
                if (targets.Contains(obs) || !obs.TryGetComponent(out Renderer rend)) continue;

                if (!tracked.TryGetValue(obs, out ObstacleData data))
                {
                    obs.TryGetComponent(out BoxCollider box);
                    data = new ObstacleData
                    {
                        origPos = obs.position,
                        origScale = obs.localScale,
                        origBottomY = rend.bounds.min.y,
                        origColSize = box ? box.size : Vector3.zero,
                        origColCenter = box ? box.center : Vector3.zero
                    };
                    tracked[obs] = data;
                }
                data.isBlocking = true;
            }
        }

        List<Transform> toRemove = new List<Transform>();

        foreach (var pair in tracked)
        {
            Transform obs = pair.Key;
            ObstacleData data = pair.Value;

            if (obs == null) { toRemove.Add(obs); continue; }

            float goal = data.isBlocking ? 1f : 0f;
            data.progress = Mathf.MoveTowards(data.progress, goal, fadeSpeed * Time.deltaTime);

            // Reset to original values before applying current frame interpolation
            obs.position = data.origPos;
            obs.localScale = data.origScale;
            obs.TryGetComponent(out Renderer rend);
            obs.TryGetComponent(out BoxCollider box);

            if (mode == Mode.Opacity)
            {
                SetAlpha(rend, Mathf.Lerp(1f, opacity, data.progress));
            }
            else
            {
                float scaleRatio = Mathf.Lerp(1f, targetScale, data.progress);
                obs.localScale = new Vector3(data.origScale.x, data.origScale.y * scaleRatio, data.origScale.z);

                float deltaY = data.origBottomY - rend.bounds.min.y;
                obs.position += Vector3.up * deltaY;

                if (box)
                {
                    box.size = new Vector3(data.origColSize.x, data.origColSize.y / scaleRatio, data.origColSize.z);
                    box.center = new Vector3(data.origColCenter.x, (data.origColCenter.y / scaleRatio) - (deltaY / obs.lossyScale.y), data.origColCenter.z);
                }
            }

            if (!data.isBlocking && data.progress <= 0f)
            {
                if (box) { box.size = data.origColSize; box.center = data.origColCenter; }
                SetAlpha(rend, 1f);
                toRemove.Add(obs);
            }
        }

        foreach (Transform obs in toRemove)
            tracked.Remove(obs);
    }

    private void SetAlpha(Renderer rend, float alpha)
    {
        if (rend == null) return;
        Color c = rend.material.color;
        c.a = alpha;
        rend.material.color = c;
    }
}