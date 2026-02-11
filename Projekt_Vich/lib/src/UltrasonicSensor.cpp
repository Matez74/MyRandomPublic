#include "UltrasonicSensor.h"

UltrasonicSensor::UltrasonicSensor(uint8_t pTrig, uint8_t pEcho)
: _pTrig(pTrig), _pEcho(pEcho) {}

void UltrasonicSensor::init() {
    pinMode(_pTrig, OUTPUT);
    pinMode(_pEcho, INPUT);
    digitalWrite(_pTrig, LOW);
}


long UltrasonicSensor::getDistance() {
    digitalWrite(_pTrig, LOW);
    delayMicroseconds(2);
    digitalWrite(_pTrig, HIGH);
    delayMicroseconds(5);
    digitalWrite(_pTrig, LOW);
    long duration = pulseIn(_pEcho, HIGH, 30000);
    if (duration == 0) return -1;
    return duration / 58.31;
}