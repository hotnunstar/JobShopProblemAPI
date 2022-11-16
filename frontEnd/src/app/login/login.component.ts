import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { utilizadores } from '../gestao-utilizadores/gestaoUtilizadoresModel';
import { AutorServiceService } from '../request/autor-service.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  
  formGroup!:FormGroup;

  utilizadorOnline!: utilizadores;
  userD!: utilizadores;
  constructor(private autorService: AutorServiceService, private router:Router) { }

  ngOnInit(): void {
   this.initForm();
  }
  initForm(){
    this.formGroup = new FormGroup({
      username: new FormControl("",[Validators.required]),
      password: new FormControl("",[Validators.required]),
    });
  }
 onSubmit(){
  if(this.formGroup.valid){
    this.autorService.login(this.formGroup.value).subscribe((result) =>{
    
      if(result._response)
      {
        alert(result._response);
        this.router.navigate(['/']);
      }
      
    this.autorService.setToken(result._token); 
    this.autorService.getToken(); 
    
    if(result._token) this.router.navigate(['/home']);
     
    });

    this.autorService.getUtilizadorObj().subscribe((dataUser)=>{
      this.userD = dataUser;
      
    });
    
    
  }
 }
 getIdUtilizador()
 {
   return this.userD.Id;
 }
}
