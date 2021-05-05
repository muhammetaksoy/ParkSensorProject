#define trigPin 3
#define echoPin 2
#define led 10
#define buzzer 11
long sure;
long mesafe;

void setup() {
pinMode(trigPin, OUTPUT);
pinMode(echoPin, INPUT);
pinMode(buzzer,OUTPUT);
pinMode(led,OUTPUT);
Serial.begin(9600);
}
void loop() {
Serial.println(mesafe);
digitalWrite(trigPin, LOW);
delayMicroseconds(2);
digitalWrite(trigPin, HIGH);
delayMicroseconds(10);
digitalWrite(trigPin, LOW);

sure = pulseIn(echoPin, HIGH);
mesafe = (sure / 2) / 29.1;

if (mesafe < 5) {
  tone(buzzer, 200);
  digitalWrite(led, HIGH);
}
else if (mesafe < 10) {
  tone(buzzer, 400);
  digitalWrite(led, HIGH);
  delay(100);
} 
else if (mesafe < 15) {
  tone(buzzer, 600);
  digitalWrite(led, HIGH);
  delay(200);
  noTone(buzzer);
  digitalWrite(led,LOW);
  delay(200);
}
else if (mesafe <= 20) {
  tone(buzzer, 400);
  digitalWrite(led, HIGH);
  delay(250);
  digitalWrite(led,LOW);
  delay(250);
 }
else{
  noTone(buzzer);
  digitalWrite(led, LOW);
  delay(300);
}
}
