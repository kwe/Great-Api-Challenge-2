import http from 'k6/http';
import { sleep } from 'k6';
3	
export default function () {
  http.get('http://localhost:8000/api/joke');
}
