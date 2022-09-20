using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollingTextFade : MonoBehaviour
{
    private TMP_Text m_TextComponent;
    public float FadeSpeed = 1.0F;
    public int RolloverCharacterSpread = 10;
    public Color ColorTint;
    void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
    }
    void Start()
    {
        StartCoroutine(AnimateVertexColors());
    }

    /// <summary>
    /// Method to animate vertex colors of a TMP Text object.
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimateVertexColors()
    {
    yield return null;
    }
}
