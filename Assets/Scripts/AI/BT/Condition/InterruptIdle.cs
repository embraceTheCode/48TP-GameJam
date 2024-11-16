using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class InterruptIdle : ConditionTask {
		
		private bool noticeableInteraction = false;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			InteractableTracker.Instance.OnNoticeableInteract += OnNoticeableInteract;
			return null;
		}

		private void OnNoticeableInteract(InteractionData obj)
		{
			noticeableInteraction = true;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			noticeableInteraction = false;
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			return noticeableInteraction;
		}
	}
}