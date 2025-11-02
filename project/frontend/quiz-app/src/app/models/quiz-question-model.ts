export interface QuizQuestion {
  category: string;
  type: string;
  difficulty: string;
  question: string;
  correct_answer: string;
  incorrect_answers: string[];
  
  // For UI purposes
  allAnswers?: string[];
  
  // Aliases for better readability in code
  correctAnswer?: string;
  incorrectAnswers?: string[];
}