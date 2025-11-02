import { Component, OnInit, HostBinding } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { QuizService } from '../../services/quiz';
import { AuthService } from '../../services/auth';
import { QuizQuestion } from '../../models/quiz-question-model';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.html',
  styleUrls: ['./quiz.scss'],
  standalone: true,
  imports: [DecimalPipe, CommonModule, RouterLink]

})
export class QuizComponent implements OnInit {
  questions: QuizQuestion[] = [];
  currentQuestionIndex = 0;
  selectedAnswer: string | null = null;
  showResults = false;
  correctAnswers = 0;
  loading = true;
  error = false;
  userAnswers: Array<{question: string, userAnswer: string, correctAnswer: string, isCorrect: boolean, category: string, difficulty: string}> = [];
  
  @HostBinding('style.--p')
  get scorePercentage(): string {
    return `${Math.round((this.correctAnswers / (this.questions.length || 1)) * 100)}%`;
  }

  constructor(private quizService: QuizService, private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    console.log('Quiz component initialized');
    this.loadQuestions();
  }

  loadQuestions(amount: number = 5): void {
    console.log('Loading questions');
    this.loading = true;
    this.error = false;
    this.quizService.getQuestions(amount).subscribe({
      next: (questions) => {
        console.log('Questions loaded:', questions);
        this.questions = questions;
        this.loading = false;
        this.resetQuiz();
      },
      error: (err) => {
        console.error('Error fetching questions:', err);
        this.error = true;
        this.loading = false;
      }
    });
  }

  resetQuiz(): void {
    this.currentQuestionIndex = 0;
    this.selectedAnswer = null;
    this.showResults = false;
    this.correctAnswers = 0;
    this.userAnswers = [];
  }

  get currentQuestion(): QuizQuestion | undefined {
    return this.questions[this.currentQuestionIndex];
  }

  selectAnswer(answer: string): void {
    this.selectedAnswer = answer;
  }

  isSelected(answer: string): boolean {
    return this.selectedAnswer === answer;
  }
  
  getDifficultyClass(): string {
    if (!this.currentQuestion || !this.currentQuestion.difficulty) {
      return 'bg-primary';
    }
    
    const difficulty = this.currentQuestion.difficulty.toLowerCase();
    return `badge-${difficulty}`;
  }

  nextQuestion(): void {
    // Check if answer is correct
    const isCorrect = this.selectedAnswer === this.currentQuestion?.correct_answer;
    if (isCorrect) {
      this.correctAnswers++;
    }

    // Store the answer
    if (this.currentQuestion) {
      this.userAnswers.push({
        question: this.currentQuestion.question,
        userAnswer: this.selectedAnswer || 'No answer',
        correctAnswer: this.currentQuestion.correct_answer,
        isCorrect: isCorrect,
        category: this.currentQuestion.category || 'Unknown',
        difficulty: this.currentQuestion.difficulty || 'Unknown'
      });
    }

    // Move to next question or end quiz
    if (this.currentQuestionIndex < this.questions.length - 1) {
      this.currentQuestionIndex++;
      this.selectedAnswer = null;
    } else {
      this.showResults = true;
      this.markQuizSolved();
    }
  }

  markQuizSolved(): void {
    const result = {
      TotalQuestions: this.questions.length,
      CorrectAnswers: this.correctAnswers,
      Percentage: (this.correctAnswers / this.questions.length) * 100,
      CompletedAt: new Date().toISOString(),
      Answers: this.userAnswers.map(a => ({
        Question: a.question,
        Category: a.category,
        Difficulty: a.difficulty,
        CorrectAnswer: a.correctAnswer,
        UserAnswer: a.userAnswer,
        IsCorrect: a.isCorrect
      }))
    };
    this.quizService.markQuizSolved(result).subscribe({
      next: () => console.log('Quiz marked as solved'),
      error: (err: any) => console.error('Error marking quiz solved:', err)
    });
  }

  restartQuiz(): void {
    this.loadQuestions();
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}