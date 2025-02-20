import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule] 
})
export class RegisterComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  role: number = 1;
  
  nameError: string = '';
  emailError: string = '';
  passwordError: string = '';
  confirmPasswordError: string = '';

  private authService = inject(AuthService);
  private router = inject(Router);

  onSubmit() {
    if (!this.validateInputs()) return;

    const userData = { name: this.name, email: this.email, password: this.password, role: this.role };

    this.authService.register(userData).subscribe({
      next: () => {
        console.log('Conta criada com sucesso!');
        this.router.navigate(['/login']);
      },
      error: () => {
        alert('Erro ao criar conta. Tente novamente!');
      }
    });
  }

  validateInputs(): boolean {
    let isValid = true;

    if (this.name.length < 3) {
      this.nameError = 'O nome deve ter pelo menos 3 caracteres.';
      isValid = false;
    } else {
      this.nameError = '';
    }

    if (!this.validateEmail(this.email)) {
      this.emailError = 'E-mail inválido.';
      isValid = false;
    } else {
      this.emailError = '';
    }

    if (this.password.length < 6) {
      this.passwordError = 'A senha deve ter pelo menos 6 caracteres.';
      isValid = false;
    } else {
      this.passwordError = '';
    }

    if (this.password !== this.confirmPassword) {
      this.confirmPasswordError = 'As senhas não coincidem!';
      isValid = false;
    } else {
      this.confirmPasswordError = '';
    }

    return isValid;
  }

  validateEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }
}
