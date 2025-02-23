using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    // 引用灯光组件
    private Light flickeringLight;

    // 正常状态下的灯光强度
    public float normalIntensity = 1f;

    // 快速闪烁时的最小和最大强度值
    public float minIntensity = 0f;
    public float maxIntensity = 2f;

    // 快速闪烁的次数
    public int flickerCount = 2;

    // 快速闪烁的间隔时间（秒）
    public float flickerInterval = 0.1f;

    // 两次快速闪烁之间的间隔时间（秒）
    public float waitTime = 3f;

    private float timer;
    private bool isFlickering = false;
    private int currentFlickerCount = 0;
    private float flickerTimer;

    void Start()
    {
        // 获取灯光组件
        flickeringLight = GetComponent<Light>();
        if (flickeringLight == null)
        {
            Debug.LogError("FlickeringLight脚本需要挂载到带有Light组件的物体上！");
        }

        // 初始化计时器
        timer = waitTime;
    }

    void Update()
    {
        // 更新计时器
        timer -= Time.deltaTime;

        // 如果计时器归零，开始快速闪烁
        if (timer <= 0f && !isFlickering)
        {
            StartFlicker();
        }

        // 如果正在快速闪烁
        if (isFlickering)
        {
            Flicker();
        }
    }

    void StartFlicker()
    {
        isFlickering = true;
        currentFlickerCount = 0;
        flickerTimer = 0f; // 重置闪烁计时器
    }

    void Flicker()
    {
        // 更新闪烁计时器
        flickerTimer += Time.deltaTime;

        // 如果到达闪烁间隔时间
        if (flickerTimer >= flickerInterval)
        {
            // 切换灯光强度
            flickeringLight.intensity = (flickeringLight.intensity == minIntensity) ? maxIntensity : minIntensity;

            // 增加闪烁计数
            currentFlickerCount++;

            // 重置闪烁计时器
            flickerTimer = 0f;
        }

        // 如果闪烁次数达到目标
        if (currentFlickerCount >= flickerCount * 2) // 乘以2是因为每次闪烁包含开和关
        {
            // 闪烁结束，恢复正常状态
            flickeringLight.intensity = normalIntensity;
            isFlickering = false;

            // 重置计时器
            timer = waitTime;
        }
    }
}