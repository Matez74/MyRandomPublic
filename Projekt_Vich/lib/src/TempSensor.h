#ifndef TEMP_SENSOR_H
#define TEMP_SENSOR_H

#include <Arduino.h>
#include <DHT.h>

class TempSensor {
public:
    TempSensor(uint8_t pin);

    void init();
    void read();

    float getTemperature();
    float getHumidity();

private:
    uint8_t _pin;
    DHT _dht;

    float _temperature;
    float _humidity;
};

#endif
