import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Validators, FormBuilder, ReactiveFormsModule }  from '@angular/forms';

interface Template {
  fieldName: string | null;
  // fieldType: string;
  expectedValue: string | null;
}

@Component({
  selector: 'app-templates-component',
  templateUrl: './templates.component.html',
  imports: [ReactiveFormsModule, CommonModule],
  standalone: true
})
export class TemplatesComponent {
  formInvalid : boolean = false; 
  template: Template[] = [];
  templateForm = this.fb.group({
    fieldName: ['', Validators.required],
    // fieldType: ['', Validators.required],
    expectedValue: ['', Validators.required],
  }
  );
  constructor(private fb: FormBuilder) {}
  onSubmit(): void {
    if (this.templateForm.invalid) {
      console.log('form invalid');
      this.formInvalid = true;
      return
    }

    console.log('submitted form');
    this.template.push(this.templateForm.value as Template);
    console.log(this.template);
  }
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
