using UnityEngine;

public class BossBee : BaseBee
{
    private BossBeeController controller;
    private float startingHealth;

    private void Start()
    {
        startingHealth = health;
        controller = GetComponent<BossBeeController>();
        damagedEvent.AddListener(controller.Knockback);
        deathEvent.AddListener(controller.Kill);
        deathEvent.AddListener(controller.KillAdds);

        EventManager.StartListening<EncounterStartEvent>(controller.StartEncounter);
    }
    private void Update()
    {
        switch (controller.currentPhase)
        {
            case (BossBeeController.Phase.Ground):
                if (health / startingHealth <= 0.5)
                {
                    controller.StartAirPhase();
                }
                break;
            case (BossBeeController.Phase.Air):
                if (controller.spawnedMeleeBees && controller.adds.Count == 0)
                {
                    controller.StartFinalPhase();
                }
                break;
        }
    }


}
