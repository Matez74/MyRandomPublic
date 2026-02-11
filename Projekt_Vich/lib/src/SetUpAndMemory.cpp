#include "SetUpAndMemory.h"

#include "UltrasonicSensor.h"
#include "ServoM.h"
#include "LcdDisplay.h"
#include "TempSensor.h"


SetUpAndMemory::SetUpAndMemory() {

}

void SetUpAndMemory::init() {
    ultrasonicSensor.init();
    lcd.init();
    tempSensor.init();
    lcd.backlight(true);
    servoM.init();
}

void SetUpAndMemory::updateValues() {
    _distance = ultrasonicSensor.getDistance();
    tempSensor.read();
    _temperature = tempSensor.getTemperature();
    _humidity = tempSensor.getHumidity();
}

void SetUpAndMemory::writeTextLCD(const char* text){
    lcd.setCursor(0,0);
    lcd.print("                ");
    lcd.setCursor(0,0);
    lcd.print(text);
}

long SetUpAndMemory::getDistance() {
    return _distance;
}

float SetUpAndMemory::getTemperature() {
    return _temperature;
}

float SetUpAndMemory::getHumidity() {
    return _humidity;
}

void SetUpAndMemory::setServoAngle(uint8_t angle) {
    _servoAngle = angle;
    servoM.write(_servoAngle);
}