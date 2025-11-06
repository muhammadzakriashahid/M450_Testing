describe('Take New Quiz', () => {
  beforeEach(() => {
    // Login first
    cy.visit('/login');
    cy.get('input[name="username"]').type('peterpan');
    cy.get('input[name="password"]').type('foobar');
    cy.get('button[type="submit"]').click();
    cy.url().should('include', '/dashboard');
  });

  it('should start a new quiz and complete it', () => {
    // Take New Quiz from dashboard
    cy.contains('button', 'Take New Quiz').click();
    
    // Verify we're on the quiz page
    cy.url().should('include', '/quiz');
    
    // Wait for questions to load
    cy.get('.quiz-card', { timeout: 10000 }).should('be.visible');
    
    // Verify quiz header is visible
    cy.contains('Question 1 of').should('be.visible');
    
    // Answer all questions
    cy.get('body').then($body => {
      const answerAllQuestions = () => {
        // Check if we're still in the quiz (not on results page)
        if ($body.find('.quiz-card').length > 0) {
          // Select first answer
          cy.get('.answer-btn').first().click();
          
          // Click next/finish button
          cy.get('.next-button').click();
          
          // Wait a bit for the next question to load
          cy.wait(500);
          
          // Recursively answer next question
          cy.get('body').then($newBody => {
            if ($newBody.find('.quiz-card').length > 0) {
              answerAllQuestions();
            }
          });
        }
      };
      
      answerAllQuestions();
    });
    
    // Verify results are displayed
    cy.contains('Quiz Complete!', { timeout: 10000 }).should('be.visible');
    cy.contains('You scored').should('be.visible');
    cy.get('.score-circle').should('be.visible');
    
    // Verify action buttons are present
    cy.contains('button', 'Take Another Quiz').should('be.visible');
    cy.contains('button', 'Go to Dashboard').should('be.visible');
  });
});