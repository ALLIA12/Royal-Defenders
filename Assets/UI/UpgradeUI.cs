using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject goldUI;
    [SerializeField] TargetLocator targetLocator;
    ParticleSystem particle;
    ParticleHandler particleHandler;
    TextMeshProUGUI displayDetails;
    Tower tower;
    public Type type;
    public enum Type
    {
        Damage,
        Rate,
        Range,
        Slow,
        Destroy
    }
    private void Start()
    {
        particle = targetLocator.bullet;
        particleHandler = particle.GetComponent<ParticleHandler>();
        displayDetails = goldUI.GetComponent<TextMeshProUGUI>();
        tower = targetLocator.GetComponent<Tower>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (goldUI == null) { return; }
        goldUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (goldUI == null)
        {
            return;
        }
        goldUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (type == Type.Damage)
        {
            displayDetails.text = $"Dmg: {particleHandler.getDamage().ToString("F2")}\n" +
                $"Cost: {targetLocator.upgradePrice}";
        }
        else if (type == Type.Rate)
        {
            // emission.rateOverTime
            var emmision = particle.emission;
            displayDetails.text = $"Rate: {emmision.rateOverTime.constant} \nCost: {targetLocator.upgradePrice}";
        }
        else if (type == Type.Range)
        {
            //shootingRange
            displayDetails.text = $"Range: {targetLocator.shootingRange} \nCost: {targetLocator.upgradePrice}";
        }
        else if (type == Type.Slow)
        {
            //bullet.GetComponent<ParticleHandler>().getSlowDownModifier();
            displayDetails.text = $"Slow: {(int)(particleHandler.getSlowDownModifier() * 100)}% \nCost: {targetLocator.upgradePrice}";
        }
        else if (type == Type.Destroy)
        {
            displayDetails.text = $"Refund: {targetLocator.upgradePrice + (tower.getTowerPrice() / 2)}";
        }
    }
}
