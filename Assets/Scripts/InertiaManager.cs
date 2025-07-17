using UnityEngine;
using UnityEngine.Rendering;

public class InertiaManager : MonoBehaviour
{
    [SerializeField] ItemsList cargoList;
    [SerializeField] Transform playerTransform;
    //[SerializeField] float maxInertiaTilt = 15f;
    //[SerializeField] float tiltSpeed = 2f; // Speed at which the cargo tilts towards the target
    //[SerializeField] float maxRotationAngle = 90f;
    [SerializeField] float positionFollowSpeed = 10f; // Speed at which the cargo follows the target
    [SerializeField] float rotationFollowSpeed = 5f;

    Transform target; // The target to follow
    //Vector3 previousPosition;
    //Vector3 velocity;
    //Vector3 targetOffset;
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        InertiaSimulation();


    }

    public void InertiaSimulation()
    {
        if (cargoList.Items.Count > 0)
        {
            for (int i = 0; i < cargoList.Items.Count; i++)
            {
                if (i == 0)
                {
                    target = playerTransform; // Primeiro item segue o jogador
                }
                else
                {
                    target = cargoList.Items[i - 1].transform;
                }

                Vector3 targetPosition = target.position + new Vector3(0, 0.3f, 0);
                cargoList.Items[i].transform.position = Vector3.Lerp(cargoList.Items[i].transform.position, targetPosition, positionFollowSpeed * Time.deltaTime);

                Quaternion targetRotation = target.rotation;

                cargoList.Items[i].transform.rotation = Quaternion.Slerp(cargoList.Items[i].transform.rotation, targetRotation, rotationFollowSpeed * Time.deltaTime);
            }
        }
    }

}


