import http from 'k6/http';
import { sleep } from 'k6';
3	
export default function () {
  http.get('http://localhost:5277/joke');
  sleep(1);
}
