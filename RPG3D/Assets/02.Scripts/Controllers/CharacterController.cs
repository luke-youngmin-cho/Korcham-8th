using UnityEngine;

namespace RPG.Controllers
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] float _speed;
        Vector3 _velocity;

        private void Update()
        {
            _velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))
                            .normalized * _speed;
        }

        private void FixedUpdate()
        {
            // 위치변화 = 속도 * 시간변화
            // 다음위치 = 현재위치 + 위치변화 = 현재위치 + 속도 * 시간변화
            transform.position += _velocity * Time.fixedDeltaTime;
        }
    }
}