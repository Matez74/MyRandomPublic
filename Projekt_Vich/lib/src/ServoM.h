#ifndef SERVO_M_H
#define SERVO_M_H

#include <Arduino.h>
#include <Servo.h>

class ServoM {
public:
    ServoM(uint8_t pin);
    void init();
    void write(uint8_t angle);

private:
    Servo serv;
    uint8_t _pin;
};

#endif