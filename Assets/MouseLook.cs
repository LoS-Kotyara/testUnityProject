using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
  // Структура, сопоставляющая имена параметрам в графическом редакторе Unity
  public enum RotationAxes
  {
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2
  }

  // Объявляем объект структуры RotationAxes
  public RotationAxes axes; 

  // Скорости вращения по осям
  public float sensitivityHor = 9.0f;
  public float sensitivityVert = 9.0f;

  // Ограничения вертикального поворота камеры
  public float minimumVert = -45.0f;
  public float maximumVert = 45.0f;

  // Угол поворота по вертикали
  private float _rotationX = 0;

  void Start()
  {
    // Отключение влияния среды на поворот персонажа
    Rigidbody body = GetComponent<Rigidbody>();
    if (body != null) // проверяем, существует ли этот компонент
      body.freezeRotation = true;
  }
  void Update()
  {
    if (axes == RotationAxes.MouseX)
    {
      transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0); // метод GetAxis() получает данные, вводимые с помощью мыши. Изменение положения курсора мыши на экране. предел -1 .. 1
    }
    else if (axes == RotationAxes.MouseY)
    {
      _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert; // увеличиваем угол поворота по вертикали в соответствии с перемещением курсора мыши

      _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); // фиксируем угол поворота в диапазоне, заданном минимальным и максимальным значениями

      float rotationY = transform.localEulerAngles.y; // сохраняем одинаковый угол вращения вокруг оси Y

      transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // создаём новый вектор из сохранённых значений поворота
    }
    else
    {
      _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
      _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

      float delta = Input.GetAxis("Mouse X") * sensitivityHor; // величина изменения угла поворота

      float rotationY = transform.localEulerAngles.y + delta; // приращение угла поворота через значение delta

      transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
  }
}