import { Component, Output, EventEmitter } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService, Users } from '../user.service';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { SpinnerComponent } from '../spinner/spinner.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule, ReactiveFormsModule, SpinnerComponent],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  @Output() isLoadingChange = new EventEmitter<boolean>();
  isLoading: boolean = false;
  isFormValid: boolean = false;
  token: string = '';
  passwordVisible = false;
  passwordPattern = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>])[A-Za-z\d!@#$%^&*(),.?":{}|<>]{8,}$/;

  constructor(private userService: UserService, private router: Router, private toastr: ToastrService) { }

  signInForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.passwordPattern)])
  })

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
  }

  signIn() {
    this.isFormValid = true;
    
    if (this.signInForm.valid) {
      this.isLoading = true;
      const formValues = this.signInForm.value;
      const newUser: Users = {
        userName: formValues.userName || " ",
        password: formValues.password || " ",
      };
      localStorage.setItem('Username', newUser.userName);
      this.userService.signIn(newUser).subscribe({
        next: response => {
          const token = response.token;
          localStorage.setItem('token', token);
          this.isLoading = false;
          this.toastr.success('Signed in successfully', 'Success', {
            timeOut: 2000,
            closeButton: true
          });
          this.router.navigate(['sideNav/dashboard']);
        },
        error: (error: HttpErrorResponse) => {
          this.isLoading = false;
          if (error.status === 401) {
            this.toastr.error('Invalid credentials', 'Error', {
              timeOut: 2000,
              closeButton: true
            });
          } else {
            console.error('Sign-in error:', error);
          }
        }
      });
    }
  }

}
