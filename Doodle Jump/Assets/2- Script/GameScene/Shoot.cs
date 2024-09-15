using UnityEngine;

public class Shoot : MonoBehaviour
{
    //To rotate
    [SerializeField] private Camera _camera;
    private Vector3 mousePos;
    
    //To shoot
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletTransform;
    
    //Cool down
    private bool _canFire;
    private float _timer;
    [SerializeField] private float _timeBetweenFire = 0.5f;

    private void Update()
    {
        //Shooting
        mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 shootDirection = mousePos - transform.position;
        float rotZ = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90f;
        if (mousePos.y > transform.position.y + 2)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        
        if (!_canFire)
        {
            _timer += Time.deltaTime;
            if (_timer > _timeBetweenFire)
            {
                _canFire = true;
                _timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && _canFire)
        {
            _canFire = false;
            Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
        }
        
    }
}
