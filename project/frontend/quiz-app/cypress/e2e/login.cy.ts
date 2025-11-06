describe('Login Page', () => {
  beforeEach(() => {
    cy.visit('/login');
  });

  it('should display login form', () => {
    cy.get('form').should('be.visible');
    cy.get('input[name="username"]').should('be.visible');
    cy.get('input[name="password"]').should('be.visible');
    cy.get('button[type="submit"]').should('be.visible');
  });

  it('should login successfully with valid credentials', () => {
    cy.get('input[name="username"]').type('peterpan');
    cy.get('input[name="password"]').type('foobar');
    cy.get('button[type="submit"]').click();
    
    // Should redirect to dashboard after successful login
    cy.url().should('include', '/dashboard');
  });

  it('should show error with invalid credentials', () => {
    cy.get('input[name="username"]').type('wronguser');
    cy.get('input[name="password"]').type('wrongpass');
    cy.get('button[type="submit"]').click();
    
    // Should stay on login page
    cy.url().should('include', '/login');
  });
  ///
  
});