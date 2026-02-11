#include "ServoM.h"

ServoM::ServoM(uint8_t pin)
: _pin(pin) {
}

void ServoM::init() {
    serv.attach(_pin);
}

void ServoM::write(uint8_t angle){
    angle = constrain(angle, 0, 180);
    serv.write(angle);
}