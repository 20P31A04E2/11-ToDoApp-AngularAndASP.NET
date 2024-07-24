import { Component } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService, Users } from '../user.service';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  isFormValid: boolean = false;
  passwordVisible = false;
  passwordPattern = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>])[A-Za-z\d!@#$%^&*(),.?":{}|<>]{8,}$/;


  constructor(private userService: UserService, private router:Router, private toastr:ToastrService) { }

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
  }

  signUpForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.passwordPattern)])
  })

  signUp(): void {
    this.isFormValid = true;

    if (this.signUpForm.valid) {
      const formValues = this.signUpForm.value;
      const newUser: Users = {
        userName: formValues.userName || " ",
        password: formValues.password || " ",
      };
      this.userService.signUp(newUser).subscribe({
        next: response => {
          if(response)
            {
              this.toastr.success('Account created successfully', 'Success',{
                timeOut:2000,
                closeButton: true
              })
              this.router.navigateByUrl('/login');
            }
          else
          this.toastr.error('User exists', 'Error',{
            timeOut:2000,
            closeButton: true

        });
        },
      });
    }
  }
}
