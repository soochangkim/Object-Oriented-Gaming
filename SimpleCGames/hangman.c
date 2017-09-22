#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <conio.h>
#include <windows.h>
#pragma warning (disable: 4996)

#define seedRandTime() (unsigned)time(NULL);
#define randize(n) rand()%n;

char answer[][20] = {
	"task goals", "social goals", "member role", "leader role",
};
int bestScore = 0;
//void showQuestions(char** questions,int qNum);

typedef struct positionundergametitle {
	int x;
	int y;
}positionUnderGameTitle;

char ** answerToMosaic();
void showGameTitle(positionUnderGameTitle * Cur);
void showGameMenu();
void showGameScreen_Title(int count, char ** questions, int n);
void gotoxy(int x, int y);
int checkAlphabet(char aChar, char ** questions, int n);
void checkAnswer(char ** questions, char * panswer, int n);

positionUnderGameTitle setMainCursor();
positionUnderGameTitle setGameCursor();
positionUnderGameTitle setStringCursor();

int main(void) {
	char ** questions;
	char aChar;
	int qNum, opNum;
	questions = answerToMosaic();

	while (1) {
		seedRandTime();
		showGameMenu();
		scanf("%d", &opNum);
		switch (opNum) {
		case 1:
			system("Cls");
			qNum = randize(4);
			for (int count = 0, qAnswer = 0; count < 7 && qAnswer == 0; ) {
				int find = 0;
				showGameScreen_Title(count, questions, qNum);
				aChar = getch();
				find = checkAlphabet(aChar, questions, qNum);
				checkAnswer(questions, &qAnswer, qNum);
				if (find == 1) continue;
				else count++;
			}
			showGameScreen_Title(7, questions, qNum);
			Sleep(5000);
			system("Cls");
			break;
		case 2:
			exit(0);
		}
	}
	
	return 0;
}


char ** answerToMosaic() {
	int n = sizeof(answer) / sizeof(answer[0]);
	char ** questions = (char **)calloc(n, sizeof(char *));
	
	for (int i = 0; i < n; i++) {
		questions[i] = (char *)calloc(sizeof(answer[i]),1);
	}

	for (int i = 0; i < n; i++) {
		for (int j = 0; answer[i][j] != '\0'; j++) {
			if (answer[i][j] == ' ')
				questions[i][j] = ' ';
			else
				questions[i][j] = '_';
		}
	}
	return questions;
}

void showGameTitle(positionUnderGameTitle * Cur) {
	gotoxy(Cur->x, Cur->y++);
	printf("*********************");
	gotoxy(Cur->x, Cur->y++);
	printf("*  H A N G   M A N  *");
	gotoxy(Cur->x, Cur->y++);
	printf("*********************");
	gotoxy(Cur->x, Cur->y++);
}

void showGameMenu() {
	positionUnderGameTitle opCur;
	opCur = setMainCursor();
	showGameTitle(&opCur);
	printf("1. Play The Game");
	gotoxy(opCur.x, opCur.y++);
	printf("2. Exit Game");
	gotoxy(opCur.x, opCur.y++);
}

void showGameScreen_Title(int count, char ** questions, int n) {
	positionUnderGameTitle opCur;
	opCur = setGameCursor();
	showGameTitle(&opCur);
	printf("Best Score: %d",bestScore);
	gotoxy(opCur.x, opCur.y++);
	printf("%d Chance left", 7- count);
	opCur = setStringCursor();
	gotoxy(opCur.x, opCur.y++);
	fputs(questions[n],stdout);
	gotoxy(opCur.x, opCur.y+=3);
	printf("Input alphabet: ");
}

void gotoxy(int x, int y) {
	COORD Cur;
	Cur.X = x;
	Cur.Y = y;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), Cur);
}

positionUnderGameTitle setMainCursor() {
	positionUnderGameTitle opCur;
	opCur.x = 30;
	opCur.y = 7;
	return opCur;
}

positionUnderGameTitle setGameCursor() {
	positionUnderGameTitle opCur;
	opCur.x = 50;
	opCur.y = 2;
	return opCur;
}

positionUnderGameTitle setStringCursor() {
	positionUnderGameTitle opCur;
	opCur.x = 30;
	opCur.y = 10;
	return opCur;
}

int checkAlphabet(char aChar, char ** questions, int n) {
	int find = 0;
	for (int i = 0; i < strlen(answer[n]); i++){
		if (aChar == answer[n][i]) {
			questions[n][i] = aChar;
			find = 1;
		}
	}
	return find;
}

void checkAnswer(char ** questions, char * panswer, int n) {
	int count = 0;
	for (int i = 0; i < strlen(answer[n]); i++) {
		if (answer[n][i] == questions[n][i]) count++;
	}
	if (count == (strlen(answer[n])+1)) *panswer = 1;
}
