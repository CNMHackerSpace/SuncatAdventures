using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    private BalloonPopReactiveTarget target;
    private  GameObject hitObject;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth/2 - size/4;
        float posY = _camera.pixelHeight/2 - size/2;
        GUI.Label(new Rect(posX, posY, size, size), "X");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(
                _camera.pixelWidth / 2,
                _camera.pixelHeight / 2,
                0);

            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100.0f);

            foreach (RaycastHit hit in hits)
            { 
                hitObject = hit.transform.gameObject;
                target = hitObject.GetComponent<BalloonPopReactiveTarget>();

                Debug.Log("Hit: " + hitObject.name);
                if (target != null)
                {
                    //Debug.Log("Target hit");
                    target.ReactToGrab();
                    // Messenger.Broadcast(GameEvent.ENEMY_HIT);
                }
                else
                {
                    // StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
        else if(Input.GetMouseButtonUp(0) && target != null)
        {
            target.ReactToRelease();
            target = null;
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}
