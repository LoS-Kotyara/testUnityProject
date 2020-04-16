using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControll : MonoBehaviour
{
  public float speed = 2.0f; // скорость перемещения персонажа
  public float gravity = -9.8f;

  private CharacterController _charController; // ссылка на компонент CharacterController

  void Start()
  {
    _charController = GetComponent<CharacterController>(); // доступ к другим компонентам, присоединённых к этому же объекту
  }


  // Update is called once per frame
  void Update()
  {
    // "Horizontal" и "Vertical" - A/D и W/S клавиши на клавиатуре соответственно
    float deltaX = Input.GetAxis("Horizontal") * speed;
    float deltaZ = Input.GetAxis("Vertical") * speed;

    Vector3 movement = new Vector3(deltaX, 0, deltaZ);
    movement.y = gravity; // заставляем персонажа ходить по земле, а не летать над ней
    
    movement = Vector3.ClampMagnitude(movement, speed); // ограничение движения по диагонали той же скорости, с которой объект перемещается параллельно осям

    movement *= Time.deltaTime; // делаем так, чтобы значение перемещения по координатам не зависело от частоты кадров

    movement = transform.TransformDirection(movement); // преобразуем вектор движения от локальных к глобальным координатам

    _charController.Move(movement); // вектор перемещает этот компонент CharacterController
  }
}
