//****************GLOBALS******************//

String unitySend;
 int i;

//GRP A
int grpARed = 2;
int grpABlue = 3;
int grpAGreen = 4;


//***********SETUP***************//

void setup() {
  
  Serial.begin(9600);
  
// GRP A
  pinMode(grpARed, OUTPUT);
  pinMode(grpABlue, OUTPUT);   
  pinMode(grpAGreen, OUTPUT); 




// ********************START BLACK****************************//
  digitalWrite(grpARed, HIGH);
  digitalWrite(grpABlue, HIGH);
  digitalWrite(grpAGreen, HIGH);

}


//********************LOOP*******************//

void loop() {

if (Serial.available() > 0) {
  
  unitySend = Serial.readString();

  i = unitySend.toInt();
  Serial.println(i);

  switch (i) {
  case 1: //GRP A RED => BLUE
      digitalWrite(grpARed, HIGH);
      digitalWrite(grpABlue, LOW);
      digitalWrite(grpAGreen, HIGH);
    break;
  case 2: //GRP A BLACK
      digitalWrite(grpARed, HIGH);
      digitalWrite(grpABlue, HIGH);
      digitalWrite(grpAGreen, HIGH);  
    break;
  case 3: //GRP A CROSS TO BLUE*****************************************
      digitalWrite(grpARed, HIGH);
      digitalWrite(grpABlue, LOW);
      digitalWrite(grpAGreen, HIGH);    
    break;
  case 4: //GRP A BLUE
      digitalWrite(grpARed, HIGH);
      digitalWrite(grpABlue, HIGH);
      digitalWrite(grpAGreen, LOW);  
    break;
                 
  default:
    break;
     }

  }
 
}
