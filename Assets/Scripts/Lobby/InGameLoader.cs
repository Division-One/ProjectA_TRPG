using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;


public class InGameLoader : Loader
{
    private static InGameLoader instance = null;
    public static InGameLoader Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }
    private void Start()
    {
        LoadTipData();
    }
    [SerializeField]
    TextMeshProUGUI tipContent;
    [SerializeField]
    Image portrait;
    List<List<object>> tipData;

    /// <summary>
    /// CSV������ �о� TipText ������ ����
    /// </summary>
    public void LoadTipData()
    {
        Debug.Log("Generate Tip");
        tipData = CSVReader.Parsing("Data/TipText");
    }

    /// <summary>
    /// 0.5�ʸ��� TipText�� update�����ִ� �ڷ�ƾ
    /// </summary>
    /// <param name="tipCount">��ü �ε� �� �ִ� �� ���� Tip�� ���̰� �� �� ����</param>
    /// <returns></returns>
    public IEnumerator UpdateTipContent(int tipCount)
    {
        HashSet<int> selectedTipIdxs = new HashSet<int>();

        float progresstPerTip = 1f / tipCount;

        int beforeProgressSection=0;
        float p = GetProgress();
        while (p < 1f)
        {
            int progressSection = (int)(p / progresstPerTip);
            if(progressSection != beforeProgressSection)
            {
                int tipIdx = Random.Range(0, tipData.Count);
                while(selectedTipIdxs.Contains(tipIdx))
                    tipIdx = Random.Range(0, tipData.Count);
                selectedTipIdxs.Add(tipIdx);
                tipContent.text = tipData[tipIdx][Constants.CSV_TIPTEXT_CONTENT_IDX].ToString();
            }

            p = GetProgress();
            beforeProgressSection = progressSection;
            yield return new WaitForSeconds(0.5f);
        }


    }
}
