#include "TempSensor.h"

#define DHT_TYPE DHT11

TempSensor::TempSensor(uint8_t pin)
    : _pin(pin), _dht(pin, DHT_TYPE),
      _temperature(NAN), _humidity(NAN) {}

void TempSensor::init() {
    _dht.begin();
}

void TempSensor::read() {
    _humidity = _dht.readHumidity();
    _temperature = _dht.readTemperature();
}

float TempSensor::getTemperature() {
    return _temperature;
}

float TempSensor::getHumidity() {
    return _humidity;
}
