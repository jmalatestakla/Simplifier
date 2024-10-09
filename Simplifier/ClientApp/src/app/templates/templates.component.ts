import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Validators, FormBuilder, ReactiveFormsModule }  from '@angular/forms';

interface Template {
  fieldName: string | null;
  fieldType: string | null;
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
    fieldType: ['', Validators.required],
    expectedValue: ['', Validators.required],
  }
  );
  constructor(private fb: FormBuilder) {}
  onSubmit(): void {
    // Validation
    if (this.templateForm.invalid) {
      console.log('form invalid');
      this.formInvalid = true;
      return
    }
    // Add new form field on frontend
    console.log('submitted form');
    this.template.push(this.templateForm.value as Template);
    console.log(this.template);
  }
  
  // Save entire form on backend.
  saveTemplate(){
      console.log('saving template');
      // REST POST request to save template
    }
}
