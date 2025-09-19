describe('Student App E2E Tests', () => {
  beforeEach(() => {
    cy.visit('http://localhost:4200'); 
  });

  it('should display the student list', () => {
    cy.contains('List Students').click();
    cy.url().should('include', '/students');
    cy.get('table').should('be.visible');
  });

  it('should add a new student', () => {
    cy.contains('Add Students').click();
    cy.url().should('include', '/addstudents');
    cy.get('#name').type('Test Student');
    cy.get('#email').type('test@example.com');
    cy.get('button[type="submit"]').click();
    cy.url().should('include', '/students');
    cy.contains('Test Student').should('be.visible');
  });
});