import { NgIf } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-nav',
  imports: [RouterModule, NgIf],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit{
  
  private _authService = inject(AuthService);
  private _router = inject(Router);
  public isLoggedIn: boolean = false;
  public user: any;

  ngOnInit(): void {
    this._authService.user$.subscribe(res => {
      this.isLoggedIn = !!res;
      this.user = res?.email;
    })
  }

  public logout(): void {
    this._authService.logout().subscribe(() => {
      this._router.navigate(['/auth/login']);
    })
  }

}
