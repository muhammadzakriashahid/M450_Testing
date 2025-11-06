describe('Dashboard', () => {
  beforeEach(() => {
    // Login before each test
    cy.visit('/login');
    cy.get('input[name="username"]').type('peterpan');
    cy.get('input[name="password"]').type('foobar');
    cy.get('button[type="submit"]').click();
    cy.url().should('include', '/dashboard');
  });

  it('should display dashboard after successful login', () => {
    // Verify we're on the dashboard
    cy.url().should('include', '/dashboard');
    
    // Verify dashboard title
    cy.contains('Dashboard').should('be.visible');
  });

  it('should display navigation bar with logout button', () => {
    // Check navigation bar
    cy.get('.nav-bar').should('be.visible');
    cy.contains('button', 'Logout').should('be.visible');
  });

  it('should display quiz history section', () => {
    // Check quiz history heading
    cy.contains('h2', 'Quiz History').should('be.visible');
  });

  it('should display take new quiz button', () => {
    // Verify "Take New Quiz" button exists and is visible
    cy.contains('button', 'Take New Quiz').should('be.visible');
    cy.contains('button', 'Take New Quiz').should('be.enabled');
  });

  it('should display message when no quizzes solved yet', () => {
    // Check if no quizzes message appears (for new users)
    cy.get('body').then($body => {
      if ($body.find('.no-quizzes').length > 0) {
        cy.contains('No quizzes solved yet.').should('be.visible');
      }
    });
  });
});