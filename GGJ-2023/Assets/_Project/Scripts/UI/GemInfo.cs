using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInfo : MonoBehaviour
{
    [SerializeField]
    private List<Texture> _numbers;
    [SerializeField]
    private UnityEngine.UI.RawImage _digit10;
    [SerializeField]
    private UnityEngine.UI.RawImage _digit01;
    [SerializeField]
    private int _gemNum;

    // Start is called before the first frame update
    void Start()
    {
        // CountGems();
        SetGemNum(_gemNum);
    }

    private void CountGems()
    {
        var gemObjs = GameObject.FindGameObjectsWithTag("Gem");
        _gemNum = gemObjs?.Length ?? 0;
    }

    // Update is called once per frame
    void Update()
    {
        // CountGems();
        if (_lastGemForDebug == _gemNum) {
            SetGemNum(_gemNum);
        }
        _lastGemForDebug = _gemNum;
    }

    public void SetGemNum(int num) {
        _digit10.texture = _numbers[num % 100 / 10];
        _digit01.texture = _numbers[num % 10];
    }

    public void DecGemNum() {
        SetGemNum(--_gemNum);
    }

    public void AddGemNum() {
        SetGemNum(++_gemNum);
    }

    int _lastGemForDebug;
}
