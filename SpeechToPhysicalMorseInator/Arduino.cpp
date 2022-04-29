#include <Servo.h>

#define SERVO 8 

Servo s; // Variável Servo
int pos; // Posição Servo

int delayLetter = 200;
int delayPressure = 200;
int delayServoPos = 2;

void setup ()
{
  s.attach(SERVO);
  Serial.begin(9600);
  s.write(0); // Inicia motor posição zero
}

void loop()
{
     if(Serial.available() > 0){
      int valueRead = Serial.read() - '0';
     if(valueRead == -35) return;

    if(valueRead == 2){
      delay(delayLetter);
      return;
    }

    for(int pos = 0; pos < 50; pos++)
    {
      s.write(pos);
      delay(delayServoPos);
    }       
    if(valueRead == 1)
    {
      delay(delayPressure);
    }
    
    for(int pos = 50; pos > 0; pos--)
    {
      s.write(pos);
      delay(delayServoPos);
    }     
  }
}