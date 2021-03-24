Урок 1. Первый микросервис.


Написать свой контроллер и методы в нем, которые бы предоставляли следующую функциональность:
1) Возможность сохранить температуру в указанное время.
2) Возможность отредактировать показатель температуры в указанное время.
3) Возможность удалить показатель температуры в указанный промежуток времени.
4) Возможность прочитать список показателей температуры за указанный промежуток времени.

1)     public IActionResult Create([FromBody] WeatherForecastDto  input) - сохранение  значений.
2)     Два варианта на запрос GET:
    2.a) обычный    public IActionResult Read().
	2.б) вариант с выборкой по датам   public IActionResult Browse([FromQuery] DateTime firstDate, [FromQuery] DateTime lastDate),
	     (обнаружил, что выдает ошибку с сервера 204 и возвращает null).
3)    public IActionResult Update([FromQuery] DateTime date , [FromQuery] int newTemperatureC) - обновление по дате
4)    public IActionResult Delete([FromQuery] DateTime firstDate, [FromQuery] DateTime lastDate) - удаление с выбранной даты начала и конца.

Проверял работоспособность в браузере PaleMoon и Postmane.
		 
	     