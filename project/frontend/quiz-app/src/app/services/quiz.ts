import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { QuizQuestion } from '../models/quiz-question-model';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  // Use the port from your backend's launchSettings.json
  private apiUrl = 'https://localhost:7124/api/quiz';

  constructor(private http: HttpClient) { }

  getQuestions(amount: number = 5): Observable<QuizQuestion[]> {
    return this.http.get<QuizQuestion[]>(`${this.apiUrl}?amount=${amount}`).pipe(
      map(questions => this.processQuestions(questions))
    );
  }

  private processQuestions(questions: QuizQuestion[]): QuizQuestion[] {
    // Prepare questions for UI display by creating allAnswers array and shuffling
    return questions.map(q => {
      // Add aliases for better readability in code
      q.correctAnswer = q.correct_answer;
      q.incorrectAnswers = q.incorrect_answers;
      
      return {
        ...q,
        allAnswers: this.shuffleArray([q.correct_answer, ...q.incorrect_answers])
      };
    });
  }

  private shuffleArray(array: string[]): string[] {
    const result = [...array];
    for (let i = result.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [result[i], result[j]] = [result[j], result[i]];
    }
    return result;
  }

  getSolvedQuizzes() {
    return this.http.get<any[]>(`${this.apiUrl}/solved`);
  }

  markQuizSolved(result: any) {
    return this.http.post(`${this.apiUrl}/solved`, result);
  }
}