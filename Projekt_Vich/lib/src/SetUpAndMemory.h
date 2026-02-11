#ifndef SETUPANDMEMORY_H
#define SETUPANDMEMORY_H

#include <Arduino.h>
#include "UltrasonicSensor.h"
#include "ServoM.h"
#include "LcdDisplay.h"
#include "TempSensor.h"

class SetUpAndMemory {
public:
    SetUpAndMemory();
    void init();
    void updateValues();

    long getDistance();
    float getTemperature();
    float getHumidity();

    void setServoAngle(uint8_t angle);
    void writeTextLCD(const char* text);

private:
    UltrasonicSensor ultrasonicSensor = UltrasonicSensor(4, 5);
    ServoM servoM = ServoM(9);
    LcdDisplay lcd = LcdDisplay();
    TempSensor tempSensor = TempSensor(11);

    float _temperature = NAN;
    float _humidity = NAN;
    long _distance = 0;
    uint8_t _servoAngle = 0;
};

#endif
