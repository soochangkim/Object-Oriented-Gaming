#include <stdio.h>
#include <Windows.h>
#include <conio.h>
#include <stdlib.h>
#include <time.h>
#include <stdbool.h>
#include <string.h>

#pragma warning (disable: 4996)
#define MaxEnemy 15
#define MaxEnemyBullet 50
#define MaxHeroBullet 3
#define setRanNum() srand((unsigned)time(NULL))
#define getRanNum(n) ((rand())%(n))
#define myAbs(a,b) ((a) >= (b) ? ((a)-(b)) : ((b)-(a)))

typedef struct enemy {
	bool eixt;
	int x, y;
	char * type;
	char frame;
	char remain;
	char beckta;
	char time;
} Enemy;
Enemy alien[MaxEnemy];

typedef struct bullet {
	bool eixt;
	int x, y;
	char style;
	char frame;
	char remain;
} Bullet;
Bullet enemyBullet[MaxEnemyBullet];
Bullet heroBullet[MaxHeroBullet];

typedef struct hero {
	int x, y;
	char * type;
}Hero;

char * typeOfEnemy[] = {
	{"[=O=]"},
	{"[**O**]"},
	{"<<==0==>>"},
	{ ";;^;;" },
	{ "oO@Oo" },
};

char hero[] = { 
	" <<A>> ",
};

void gotoxy(int x, int y);
bool checkKeyPush(int key);


int main(void) {
	Hero myFigher[2];
	Enemy * pa, *cpa;
	Bullet * hb, * eb;
	char ch;
	int found = 0;
	int i, j;
	int score = 0;

	myFigher[1].type = hero;
	myFigher[1].x = 38;
	myFigher[1].y = 23;

	setRanNum();
	for (int count = 1;; count++) {
		//hero move
		myFigher[0].x = myFigher[1].x;
		myFigher[0].y = myFigher[1].y;
		if (count % 5 == 0) {
			if (checkKeyPush(VK_LEFT)) {
				if (myFigher[1].x > 1) myFigher[1].x--;
			}
			if (checkKeyPush(VK_RIGHT)) {
				if (myFigher[1].x < 72) myFigher[1].x++;
			}
			if (checkKeyPush(VK_UP)) {
				if (myFigher[1].y > 18) myFigher[1].y--;
			}
			if (checkKeyPush(VK_DOWN)) {
				if (myFigher[1].y < 23) myFigher[1].y++;
			}
			if (checkKeyPush(VK_SPACE)) {
				hb = heroBullet;
				for (i = 0; i < sizeof(heroBullet) / sizeof(heroBullet[0]); i++, hb++) {
					if (hb->eixt == false) {
						hb->eixt = true;
						hb->x = myFigher[1].x + strlen(myFigher[1].type)/2 ;
						hb->y = myFigher[1].y -1;
						hb->style = '@';
						break;
					}
				}
			}
		}
		if (kbhit()) {
			ch = getch();
			if (ch == 0xE0 || ch == 0) getch();
			else if (ch == 27) {
				return 0;
			}
		}
		//make alien ships
		pa = alien;
		if (getRanNum(100) == 0) {
			for (i = 0; i < MaxEnemy; i++, pa++) {
				if (pa->eixt == false) {
					pa->type = typeOfEnemy[getRanNum(sizeof(typeOfEnemy) / sizeof(typeOfEnemy[0]))];
					if (getRanNum(2) == 0) { pa->x = 1; pa->beckta = 1; }
					else {
						pa->x = 80 -strlen(pa->type) - 1; pa->beckta = -1;
					}
					while (1) {
						pa->y = getRanNum(MaxEnemy);
						found = 0;
						cpa = alien;
						for (int j = 0; j < MaxEnemy; cpa++, j++) {
							if (cpa->eixt == true) {
								if (cpa->y == pa->y) {
									found = 1;
									break;
								}
							}
						} 
						if (found == 0) break;
					}
					pa->frame = pa->remain = getRanNum(10) + 5;
					pa->eixt = true;
					pa->time = 5;
					break;
				}
			}
		}

		//display alien ships
		pa = alien;
		for (i = 0; i < MaxEnemy; i++, pa++) {
			if (pa->eixt == true) {
				if (--pa->remain == 0) {
					pa->remain = pa->frame;
					gotoxy(pa->x, pa->y); puts("           ");
					pa->x += pa->beckta;
					gotoxy(pa->x, pa->y); puts(pa->type);
					if (((pa->x == 0) && (pa->beckta == -1)) || (pa->x == (79 - strlen(pa->type)))&&(pa->beckta == 1)) {
						gotoxy(pa->x, pa->y); puts("           ");
						pa->eixt = false;
					}
				}
				//if (pa->time == 5) { ; } 
				if (getRanNum(20) == 0) {
					eb = enemyBullet;
					for (j = 0; j < MaxEnemyBullet; j++,eb++) {
						if (eb->eixt == false) {
							eb->eixt = true;
							eb->style = '*';
							eb->x = pa->x + strlen(pa->type)/2;
							eb->y = pa->y + 1;
							eb->frame = eb->remain = pa->frame / 5;
						}
					}
				}
			}
		}
		//display hero bullet
		hb = heroBullet;
		for (i = 0; i < MaxHeroBullet; i++, hb++) {
			if (hb->eixt == true) {
				//if (count % 3) break;
				gotoxy(hb->x, hb->y--); putch(' ');
				if (hb->y < 0) hb->eixt = false;
				else {
					gotoxy(hb->x, hb->y);
					putch(hb->style);
				}
			}
		}
		
		//display alien bullet
		eb = enemyBullet;
		for (i = 0; i < MaxEnemyBullet; i++, eb++) {
			if (eb->eixt == true) {
				if (--eb->remain == 0) {
					eb->remain = eb->frame;
					gotoxy(eb->x, eb->y++); putch(' ');
					if (eb->y > 23) eb->eixt = false;
					else {
						gotoxy(eb->x, eb->y); putch(eb->style);
					}
				}
			}
		}
		
		//hit alien

		hb = heroBullet;
		for (i = 0; i < MaxHeroBullet; i++, hb++) {
			if (hb->eixt == true) {
				pa = alien;
				for (j = 0; j < MaxEnemy; j++, pa++) {
					if (pa->eixt == true) {
						if (hb->y == pa->y) {
							if (pa->x <= hb->x && (pa->x +strlen(pa->type) -1) >= hb->x) {
								gotoxy(pa->x, pa->y);
								puts("         ");
		//						gotoxy(pa->x, pa->y);
		//						puts(" .,:,. ");
								score += 200 / pa->frame;
								pa->eixt = false;
								hb->eixt = false;
							}
						}
					}
				}
			}
		}

		eb = enemyBullet;
		for (i = 0; i < MaxEnemyBullet; i++, eb++) {
			if (eb->eixt == true) {
				if (eb->y == myFigher[1].y) {
					if (myAbs((myFigher[1].x + strlen(myFigher[1].type) / 2), eb->x) <= 2) {
						gotoxy(myFigher[1].x, myFigher[1].y - 1);
						puts("   .   ");
						gotoxy(myFigher[1].x, myFigher[1].y );
						puts(" .  . .");
						gotoxy(myFigher[1].x, myFigher[1].y + 1);
						puts("..:V:..");
						Sleep(2000);
						return 0;
					}
				}
			}
		}

		gotoxy(myFigher[0].x, myFigher[0].y);
		puts("       ");
		gotoxy(myFigher[1].x, myFigher[1].y);
		puts(myFigher[1].type);
		gotoxy(0, 24);
		printf("Score: %d", score);
		Sleep(20);
	} 
	return 0;
}

void gotoxy(int x, int y){
	COORD Cur;
	Cur.X = x;
	Cur.Y = y;
	SetConsoleCursorPosition (GetStdHandle(STD_OUTPUT_HANDLE), Cur);
}

bool checkKeyPush(int key) {
	return ((GetAsyncKeyState(key) & 0x8000) != 0);
}
