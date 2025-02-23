using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    // ���õƹ����
    private Light flickeringLight;

    // ����״̬�µĵƹ�ǿ��
    public float normalIntensity = 1f;

    // ������˸ʱ����С�����ǿ��ֵ
    public float minIntensity = 0f;
    public float maxIntensity = 2f;

    // ������˸�Ĵ���
    public int flickerCount = 2;

    // ������˸�ļ��ʱ�䣨�룩
    public float flickerInterval = 0.1f;

    // ���ο�����˸֮��ļ��ʱ�䣨�룩
    public float waitTime = 3f;

    private float timer;
    private bool isFlickering = false;
    private int currentFlickerCount = 0;
    private float flickerTimer;

    void Start()
    {
        // ��ȡ�ƹ����
        flickeringLight = GetComponent<Light>();
        if (flickeringLight == null)
        {
            Debug.LogError("FlickeringLight�ű���Ҫ���ص�����Light����������ϣ�");
        }

        // ��ʼ����ʱ��
        timer = waitTime;
    }

    void Update()
    {
        // ���¼�ʱ��
        timer -= Time.deltaTime;

        // �����ʱ�����㣬��ʼ������˸
        if (timer <= 0f && !isFlickering)
        {
            StartFlicker();
        }

        // ������ڿ�����˸
        if (isFlickering)
        {
            Flicker();
        }
    }

    void StartFlicker()
    {
        isFlickering = true;
        currentFlickerCount = 0;
        flickerTimer = 0f; // ������˸��ʱ��
    }

    void Flicker()
    {
        // ������˸��ʱ��
        flickerTimer += Time.deltaTime;

        // ���������˸���ʱ��
        if (flickerTimer >= flickerInterval)
        {
            // �л��ƹ�ǿ��
            flickeringLight.intensity = (flickeringLight.intensity == minIntensity) ? maxIntensity : minIntensity;

            // ������˸����
            currentFlickerCount++;

            // ������˸��ʱ��
            flickerTimer = 0f;
        }

        // �����˸�����ﵽĿ��
        if (currentFlickerCount >= flickerCount * 2) // ����2����Ϊÿ����˸�������͹�
        {
            // ��˸�������ָ�����״̬
            flickeringLight.intensity = normalIntensity;
            isFlickering = false;

            // ���ü�ʱ��
            timer = waitTime;
        }
    }
}