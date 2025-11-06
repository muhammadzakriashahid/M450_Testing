import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule, DatePipe, DecimalPipe } from '@angular/common';
import { QuizService } from '../../services/quiz';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, DatePipe, DecimalPipe],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class DashboardComponent implements OnInit {
  quizzes: any[] = [];
  expandedQuizzes: Set<number> = new Set();

  constructor(private quizService: QuizService, private auth: AuthService, private router: Router) {}

  ngOnInit() {
    console.log('Dashboard init');
    this.quizService.getSolvedQuizzes().subscribe({
      next: data => {
        console.log('Solved quizzes:', data);
        this.quizzes = data;
      },
      error: err => console.error('Error fetching solved quizzes:', err)
    });
  }

  toggleQuiz(quizId: number) {
    if (this.expandedQuizzes.has(quizId)) {
      this.expandedQuizzes.delete(quizId);
    } else {
      this.expandedQuizzes.add(quizId);
    }
  }

  isExpanded(quizId: number): boolean {
    return this.expandedQuizzes.has(quizId);
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  goToQuiz() {
    this.router.navigate(['/quiz']);
  }
}