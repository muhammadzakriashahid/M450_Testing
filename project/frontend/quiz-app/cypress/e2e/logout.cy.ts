describe('Logout', () => {
  it('should login and then logout successfully', () => {
    // Login
    cy.visit('/login');
    cy.get('input[name="username"]').type('peterpan');
    cy.get('input[name="password"]').type('foobar');
    cy.get('button[type="submit"]').click();
    
    // Verify we're on dashboard
    cy.url().should('include', '/dashboard');
    
    // Click logout button
    cy.contains('button', 'Logout').click();
    
    // Verify we're back on login page
    cy.url().should('include', '/login');
  });
});